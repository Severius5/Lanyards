var generateSignature = function (callback, params_to_sign) {
	$.ajax({
		url: '/signature',
		type: 'GET',
		dataType: 'text',
		xhrFields: { withCredentials: true },
		data: { data: params_to_sign },
		success: function (signature, textStatus, xhr) { callback(signature); },
	});
}

var applyImgUploadWidget = function (btnId, inputId) {
	var widget = cloudinary.createUploadWidget({
		cloudName: 'todo: somehow get it from config',
		apiKey: 'todo: somehow get it from config',
		uploadPreset: 'lanyards',
		cropping: true,
		multiple: false,
		croppingShowDimensions: true,
		croppingAspectRatio: 10.5,
		resourceType: 'image',
		clientAllowedFormats: ['png', 'jpg', 'jpeg'],
		sources: ['local'],
		thumbnails: false,
		uploadSignature: generateSignature
	}, (error, result) => {
		if (!error && result && result.event === 'success') {
			$(inputId).attr('value', result.info.secure_url);
		}
	});

	$(btnId).click(function (e) {
		e.preventDefault();
		widget.open();
	});
}