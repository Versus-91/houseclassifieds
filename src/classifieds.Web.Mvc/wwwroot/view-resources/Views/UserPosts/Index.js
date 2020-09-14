Dropzone.autoDiscover = false;

$(document).ready(function () {
	var priceMask = new AutoNumeric('#price', { currencySymbol: '$' });
	var depositMask = new AutoNumeric('#deposit', { currencySymbol: '$' });
	var form = $("#CreateAd");
	$("body").persianNum();
	realtime($("#area"));
	var submitButton = $("#submitButton");
	var marker = {};
	var depositFormField = $("#depositField");
	var BedField = $("#bedroom");
	var depositInput = $("#deposit");
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

	//dropzone uploader script
	var dropZone = new Dropzone('#dropzone', {
		autoProcessQueue: false,
		maxFiles: 8,
		preventDuplicates: true,
		acceptedFiles: 'image/*',
		maxFilesize: 4,
		previewTemplate: document.querySelector('#tpl').innerHTML,
		//addRemoveLinks: true,
		init: function () {
			this.on('error', function (file, errorMessage) {
				if (errorMessage.indexOf('big') !== -1) {
					var errorDisplay = document.querySelectorAll('[data-dz-errormessage]');
					console.log(errorDisplay)
					errorDisplay[errorDisplay.length - 2].innerHTML = 'حجم فایل زیاد است.' + '<br/>حداکثر حجم فایل مجاز :' + this.options.maxFilesize + 'مگابایت ';
				}
			});
		},
		url: "/ads/create",
		headers: {
			'X-XSRF-TOKEN': $('input[name="__RequestVerificationToken"]').val()
		},
		success: function (file, response) {
			window.location.href = "../";
		}
	});
	//remove duplcate files
	dropZone.on("addedfile", function (file) {
		if (this.files.length) {
			var _i, _len;
			for (_i = 0, _len = this.files.length; _i < _len - 1; _i++) // -1 to exclude current file
			{
				if (this.files[_i].name === file.name && this.files[_i].size === file.size && this.files[_i].lastModifiedDate.toString() === file.lastModifiedDate.toString()) {
					this.removeFile(file);
				}
			}
		}
	});
	//submit form
	submitButton.click((e) => {
		e.preventDefault();
		var formData = new FormData();
		var object = {};
		if (!form.valid()) {
			return;
		}
		submitButton.addClass("is-loading");
		var items = form.serializeArray();
		for (var i = 0; i < items.length; i++) {
			if (items[i].name.toLowerCase() != "price" && items[i].name.toLowerCase() != "deposit") {
				console.log('if:', items[i].name.toLowerCase());
				formData.append(items[i].name, items[i].value);
			}
			else {
				console.log('else:', items[i].name.toLowerCase());
				formData.append('Price', priceMask.getNumericString());
				formData.append('Deposit', depositMask.getNumericString());
			}
		}
		if (!$.isEmptyObject(marker)) {
			var coordinates = marker.getLatLng();
			formData.append('Latitude', coordinates.lat);
			formData.append('Longitude', coordinates.lng);
		}
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
