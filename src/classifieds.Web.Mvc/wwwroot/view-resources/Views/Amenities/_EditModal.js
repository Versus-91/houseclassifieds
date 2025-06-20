﻿(function ($) {
    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });
    var _cityService = abp.services.app.amenity,
        l = abp.localization.getSource('classifieds'),
        _$modal = $('#AmenityEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var city = _$form.serializeFormToObject();
        abp.ui.setBusy(_$form);
        _cityService.update(city).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('amenity.edited', city);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    //_$form.closest('div.modal-content').find(".save-button").click(function (e) {
    //    e.preventDefault();
    //    save();
    //});

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);
