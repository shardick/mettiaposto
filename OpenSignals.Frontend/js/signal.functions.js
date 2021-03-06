﻿/*
* SIGNALS FUNCTIONS
*/

var currentSignalID = 0;
var mapManager;
var nearbyLoaded = false;
var socialUser;

function initSubmitPage()
{
    $(document).ready(function ()
    {
        mapManager = $.mapManager();

        $('#address').focus();
        $('#tabs').tabs({
            show: function (event, ui)
            {
                var mapOpts = {
                    zoom: currentCity.zoom,
                    scaleControl: false, mapTypeControl: false, disableDefaultUI: true, disableDoubleClickZoom: true,
                    scrollwheel: false, streetViewControl: false
                };

                if ($(ui.panel).attr('id') == 'map')
                {
                    mapManager.createMap({ container: 'map', lat: currentCity.lat, lng: currentCity.lng, googleOptions: mapOpts });

                    if ($('#txtAddress').val() != '')
                        geolocateByAddress();
                }
                else
                {
                    getSignalsNeraby(currentMarker.zip);
                }
            }
        });

        $("#criticalSlider").slider({
            value: 1,
            min: 1,
            max: 3,
            step: 1,
            slide: function (event, ui)
            {
                switch (ui.value)
                {
                    case 1:
                        $("#criticalLevel").html('Normale');
                        break;
                    case 2:
                        $("#criticalLevel").html('Media');
                        break;
                    case 3:
                        $("#criticalLevel").html('Alta');
                        break;
                }
            }
        });
    });
}

function saveSignal()
{
    if (!mapManager.getMap('map'))
    {
        alert('Mappa non inizializzata');
        return false;
    }

    if (!mapManager.getMarker('geoLocatedMarker0'))
    {
        alert('Inserire un indirizzo o impostare correttamente il marker sulla mappa');
        return false;
    }

    validation = $.validateUtils({
        errorStyle: 'border-color:Red', errorDiv: '#messages', showAs: 'div', headerMessage: 'Alcuni campi non sono compilati correttamente'
    });

    validation.addRule({
        field: '#txtAddress', validateFunction: 'notEmpty', message: 'Indirizzo'
    });
    validation.addRule({
        field: '#txtSubject', validateFunction: 'notEmpty', message: 'Oggetto'
    });
    validation.addRule({
        field: '#txtDescription', validateFunction: 'notEmpty', message: 'Descrizione'
    });
    validation.addRule({
        field: '#txtName', validateFunction: 'notEmpty', message: 'Nome'
    });
    validation.addRule({
        field: '#txtEmail', validateFunction: 'notEmpty', message: 'E-Mail vuota'
    });
    validation.addRule({
        field: '#txtEmail', validateFunction: 'validEmail', message: 'E-Mail non valida'
    });
    validation.addRule({
        field: '#ddlCategories', validateFunction: 'checkSelected', nullValue: '-1', message: 'Categoria'
    });

    validation.validate();

    if (validation.validationResult())
    {
        $('#submitForm').hide();

        writeAjax('#messages');

        addSignal();
    }
    else
        validation.showErrorMessage();
}

function addSignal()
{
    var proxy = new JSONService();

    var s = new Object();
    s.subject = $('#txtSubject').val();
    s.description = $('#txtDescription').val();
    s.showName = document.getElementById('chkPublicName').checked;
    s.categoryID = $('#ddlCategories').val();
    s.email = $('#txtEmail').val();
    s.latitude = mapManager.getMarker('geoLocatedMarker0').obj.getPosition().lat();
    s.name = $('#txtName').val();
    s.longitude = mapManager.getMarker('geoLocatedMarker0').obj.getPosition().lng();
    s.address = $('#txtAddress').val();
    
    if (mapManager.getAddressComponent(completeAddress, 'postal_code').long_name == '')
        s.zip = 0;
    else
        s.zip = mapManager.getAddressComponent(completeAddress, 'postal_code').long_name;

    s.city = currentCity.name;
    s.zoom = mapManager.getZoom('map');
    s.attachment = '';
    s.criticalLevel = $('#criticalSlider').slider('option', 'value');
    s.source = 'www';

    if ($('#fuFile').val() != '')
    {
        $.ajaxFileUpload
        (
            {
                url: composeAjaxUploadUrl(),
                secureuri: false,
                fileElementId: 'fuFile',
                dataType: 'json',
                success: function (data, status)
                {
                    if (data.error)
                    {
                        if (data.error == 'ERROR')
                            alert(data.errorMessage);
                        else if (data.error = 'WRONG_EXT')
                            alert('Il file caricato non è di tipo immagine');
                        else if (data.error == 'NO_FILE')
                            alert('Il file selezionato non è stato inviato correttamente');
                        else if (data.error == 'TOO_MUCH')
                            alert('Il file selezionato è troppo grande: dimensione massima 10MB');

                        hideAjax('#messages');
                        $('#submitForm').show();
                    }
                    else
                    {
                        s.attachment = data.fileName;
                        proxy.addSignal(s, ajaxSessionKey, addSignal_callback);
                    }
                },
                error: function (data, status, e)
                {
                    alert(e);
                }
            });
    }
    else
        proxy.addSignal(s, ajaxSessionKey, addSignal_callback);
}

function geolocateByAddress()
{
    if ($('#txtAddress').val() != '')
    {
        var options =
    mapManager.geolocate(
        { address: $('#txtAddress').val() + ", " + currentCity.name,
            mapID: 'map',
            callback: function (response, status)
            {
                geolocationByAddress_callback(response, status, { map: 'map' });
            }
        });
    }
}

function geolocationByAddress_callback(r, status, options)
{
    if (mapManager.checkGeolocationResult(status))
    {
        var data = mapManager.getGeolocationData(r, 0);

        if (mapManager.getAddressComponent(data.address_components, 'locality').long_name == currentCity.name)
        {
            switch (data.types[0])
            {
                case 'street_address':
                    mapManager.setZoom({ mapID: 'map', zoom: 16 });
                    break;
                case 'postal_code':
                    mapManager.setZoom({ mapID: 'map', zoom: 14 });
                    break;
                case 'sublocality':
                    alert("L'indirizzo inserito non è molto preciso, prova a riscriverlo indicando precisamente il tipo di indirizzo (via, viale, piazza) o il numero civico");
                    return;
                    break;
                case 'route':
                    mapManager.setZoom({ mapID: 'map', zoom: 15 });
                    break;
                case 'locality':
                    alert("L'indirizzo inserito non è molto preciso, prova a riscriverlo indicando precisamente il tipo di indirizzo (via, viale, piazza) o il numero civico");
                    return;
                    break;
            }

            mapManager.setCenter({ mapID: 'map', position: data.geometry.location });

            switch (data.types[0])
            {
                case 'street_address':
                case 'route':
                case 'postal_code':
                    mapManager.addMarker({
                        managerInstance: mapManager,
                        id: 'geoLocatedMarker0', position: data.geometry.location, draggable: true, mapID: 'map',
                        image: MARKERIMAGE_ALERT, center: true, localize: true,
                        goelocalizationCallback: function (response, status) { geoLocationByLatLng_callback(response, status); },
                        dragEnd: function (event)
                        {
                            mapManager.geolocate({ mapID: 'map', position: event.latLng, callback:
                            function (response, status) { geoLocationByLatLng_callback(response, status); }
                            });
                        }
                    });
                    break;
            }
        }
        else
            alert("L'indirizzo che hai inserito non è a " + currentCity.name);
    }
    else
        alert("L'indirizzo inserito non è molto preciso, prova a riscriverlo indicando precisamente il tipo di indirizzo (via, viale, piazza) o il numero civico");

    currentMap = null;
}

function geoLocationByLatLng_callback(response, status)
{
    if (mapManager.checkGeolocationResult(status))
    {
        var data = mapManager.getGeolocationData(response, 0);

        completeAddress = data.address_components;

        if (mapManager.getAddressComponent(data.address_components, 'locality').long_name == currentCity.name)
        {
            var address = mapManager.getAddressComponent(data.address_components, 'route').long_name;
            if (mapManager.getAddressComponent(data.address_components, 'street_number').long_name != '')
                address += ', ' + mapManager.getAddressComponent(data.address_components, 'street_number').long_name;

            $('#txtAddress').val(address);

            $('#completeAddress').show();
            $('#completeAddress').html('Indirizzo completo: ' + data.formatted_address);
        }
        else
            alert("L'indirizzo che hai inserito non è a " + currentCity.name);
    }
    else
        alert("Indirizzo non trovato, prova ad essere più preciso");
}

function addSignal_callback(r)
{
    hideAjax('#messages');

    if (r.error)
    {
        writeError(r.error.message, '#messages');
    }
    else if (r.result)
    {
        var text = 'La segnalazione è stata salvata correttamente.<br/><a href="/' + r.result.city.toLowerCase() + '/' + r.result.signalID + '/segnalazione.aspx">Clicca qui</a> per visualizzare la pagina di dettaglio.';

        goTo('#message');
        writeMessage('Segnalazione salvata correttamente', text, '#messages');
    }
}

function getSignalsNeraby(zipCode)
{
    if (!nearbyLoaded)
    {
        $('#mapNearby').show();
        var proxy = new JSONService();
        proxy.getSignalsNearby({ zip: zipCode, signalID: currentMarker.id, ajaxSessionKey: ajaxSessionKey }, getSignalsNearby_callback);
    }
}

function getSignalsNearby_callback(r)
{
    if (r.error)
    {
        alert(r.error.message);
    }
    else if (r.result)
    {
        if (r.result.length > 0)
        {
            var mapOpts = {
                zoom: currentCity.zoom,
                scaleControl: false, mapTypeControl: false, scrollwheel: false, streetViewControl: false
            };

            mapManager.createMap({ container: 'mapNearby', lat: currentCity.lat, lng: currentCity.lng, googleOptions: mapOpts });

            var bounds = new google.maps.LatLngBounds();

            for (var i = 0; i < r.result.length; i++)
            {
                var s = r.result[i];
                var signal = s.signal;

                var myLatLng = new google.maps.LatLng(signal.latitude, signal.longitude);

                bounds.extend(myLatLng);

                var image = '';
                if (s.signal.status == 2)
                    image = MARKERIMAGE_OK;
                else
                    image = MARKERIMAGE_ALERT;

                m = mapManager.addMarker({ id: 'signalMarker' + signal.signalID, position: myLatLng, draggable: false, mapID: 'mapNearby', image: image });
                m.html = s.description;
                w = new google.maps.InfoWindow({ content: s.description, maxWidth: 300 });
                google.maps.event.addListener(m, 'click', function () { w.setContent(this.html); w.open(mapManager.getMap('mapNearby').obj, this) });
            }

            mapManager.fitZoomToBounds({ mapID: 'mapNearby', zoom: 15 });

            mapManager.fitBounds({ mapID: 'mapNearby', bounds: bounds });
        }

        nearbyLoaded = true;
    }
}

function initDetailPage()
{
    $(document).ready(function ()
    {
        getComments(0);
        mapManager = $.mapManager();

        $('#tabs').tabs({
            show: function (event, ui)
            {
                if ($(ui.panel).attr('id') == 'map')
                {
                    var mapOpts = {
                        zoom: currentCity.zoom,
                        scaleControl: false, mapTypeControl: false, disableDefaultUI: true, disableDoubleClickZoom: true,
                        scrollwheel: false, streetViewControl: false
                    };

                    var mapOpts = {
                        zoom: currentMarker.zoom,
                        scaleControl: false, mapTypeControl: false, disableDefaultUI: true, disableDoubleClickZoom: true,
                        scrollwheel: false, streetViewControl: false
                    };

                    mapManager.createMap({ container: 'map', lat: currentMarker.lat, lng: currentMarker.lng, googleOptions: mapOpts });

                    var image = '';

                    if (currentMarker.status == 2)
                        image = MARKERIMAGE_OK;
                    else
                        image = MARKERIMAGE_ALERT;

                    mapManager.addMarker({ id: 'signalMarker' + currentMarker.id, position: new google.maps.LatLng(currentMarker.lat, currentMarker.lng),
                        image: image, center: true, zoom: true, zoomValue: currentMarker.zoom, mapID: 'map'
                    });
                }
                else
                {
                    getSignalsNeraby(currentMarker.zip);
                }
            }
        });

        fbInit();

        $('#lnkPhoto').fancybox();

        $('#subscribeDialog').dialog({
            width: 470, autoOpen: false, title: 'Rimani aggiornato sulla segnalazione via email', resizable: false, draggable: false,
            modal: true, buttons: { 'Aggiornami': function () { subscribeSignal(); } }
        });
    });
}

function openSubscribeDialog()
{
    $('#subscribeDialog').dialog('open');
}

function openChangeSignalStatus(newStatus)
{
    switch (newStatus)
    {
        case 1:
            $('#newStatus').html('Segnalazione aperta');
            break;
        case 2:
            $('#newStatus').html('Segnalazione chiusa');
            break;
        case 4:
            $('#newStatus').html('Segnalazione riaperta');
            break;
    }

    $('#changeStatusDialog').dialog({
        width: 470, title: 'Modifica lo stato della segnalazione', resizable: false, draggable: false, modal: true,
        buttons: { 'Modifica stato': function () { changeStatus(newStatus); } }
    });

}

function changeStatus(newStatus)
{
    $('#changeStatusForm').hide();
    writeAjax('#changeStatusMessages');
    var proxy = new JSONService();
    proxy.changeSignalStatus(currentMarker.id, newStatus, $('#txtChangeStatusDescription').val(), ajaxSessionKey, changeStatus_callback);
}

function changeStatus_callback(r)
{
    if (r)
    {
        if (r.error)
        {
            alert(r.error.message);
        }
        else
        {
            $('#changeStatusDialog').dialog('destroy');
            document.location.reload();
        }
    }
}