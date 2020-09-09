Dropzone.autoDiscover = false;

$(document).ready(function () {
	new AutoNumeric.multiple('.currency', { currencySymbol: '$' });
	var form = $("#CreateAd");
	$("body").persianNum();
	var submitButton = $("#submitButton");
	var marker = {};
	var depositFormField = $("#deposit");
	var BedField = $("#bedroom");
	var depositInput = $("#depostInput");
	if ($("#category option:selected").text().includes("رهن")) {
		depositFormField.show();
	} else {
		depositFormField.hide();
	}
	if ($("#type option:selected").text().includes("اپارتمان") || $("#type option:selected").text().includes("ویلایی")) {
		BedField.show();
	} else {
		BedField.hide();
	}
	$("#type").change(function (e) {
		if ($("#type option:selected").text().includes("اپارتمان") || $("#type option:selected").text().includes("ویلایی")) {
			BedField.show();
		} else {
			BedField.hide();
		}
		depositInput.val('');
	});
	$("#category").change(function (e) {
		if ($("#category option:selected").text().includes("رهن")) {
			depositFormField.show();
		} else {
			depositFormField.hide();
		}
		depositInput.val('');
	});
	var map = L.map('map').setView([34.543896, 69.160652], 13);
	L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
		attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
	}).addTo(map);
	map.on('click', onMapClick);
	function showPosition(position) {
		console.log('position')
		console.log("asdasd:", position.coords)
		map.setView([position.coords.latitude, position.coords.longitude])
		marker = L.marker({ lat: position.coords.latitude, lng: position.coords.longitude }).addTo(map)
			.bindPopup('محل انتخاب شده .')
			.openPopup();
	}
	function onMapClick(e) {
		if (marker != undefined) {
			map.removeLayer(marker);
		}
		marker = L.marker(e.latlng).addTo(map)
			.bindPopup('محل انتخاب شده .')
			.openPopup();
	}

	function showError(error) {
		console.log('getCurrentPosition returned error', error);
	}
	navigator.geolocation.getCurrentPosition(showPosition, showError, { timeout: 10000 });

	var dropZone = new Dropzone('#dropzone', {
		autoProcessQueue: false,
		maxFiles: 8,
		acceptedFiles: 'image/*',
		maxFilesize: 1,
		previewTemplate: document.querySelector('#tpl').innerHTML,
		//addRemoveLinks: true,
		url: "/ads/create",
		headers: {
			'X-XSRF-TOKEN': $('input[name="__RequestVerificationToken"]').val()
		},
		success: function (file, response) {
			window.location.href = "../";
		}
	});
	submitButton.click((e) => {
		e.preventDefault();
		var formData = new FormData();
		if (!form.valid()) {
			return;
		}
		submitButton.addClass("is-loading");
		var items = form.serializeArray();
		for (var i = 0; i < items.length; i++) {
			formData.append(items[i].name, items[i].value);
		}
		if (!$.isEmptyObject(marker)) {
			var coordinates = marker.getLatLng();
			formData.append('Latitude', coordinates.lat);
			formData.append('Longitude', coordinates.lng);
		}
		var object = {};
		formData.forEach(function (value, key) {
			object[key] = value;
		});
		$.ajax({
			type: "POST",
			beforeSend: function (xhr) {
				xhr.setRequestHeader('X-XSRF-TOKEN', $('input[name="__RequestVerificationToken"]').val());
			},
			url: "/ads/create",
			contentType: 'application/json',
			dataType: "json",
			data: JSON.stringify(object), // serializes the form's elements.
			success: function (data) {
				dropZone.options.url = '/stream/upload/' + data.result
				dropZone.processQueue();
				console.log(data); // show response from the php script.
			}, error: function (err) {
				console.log(err);
				submitButton.removeClass("is-loading");

			}
		}).done(function () {
			submitButton.removeClass("is-loading");
		});

	})

});
