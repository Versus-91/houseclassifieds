(function ($) {
    _$areaSelect = $("#selectAreaEdit");
    $('.select2').select2({ theme: 'bootstrap4' });

    $('#selectCityEdit').on('change', function (e) {
        getAreas(e.target.value);
    });
    var defaultCity = $('#selectCityEdit').find(":selected").val();
    if (!!defaultCity) {
        getAreas(defaultCity);
	}
    function getAreas(id) {
        if (id != 0) {
            _$areaSelect.prop("disabled", false);
            $.get('/api/services/app/Area/GetAreaByCityId?cityId=' + id, function (res) {
                var items = res.result.items.map(m => {
                    return {
                        id: m.id,
                        text: m.name
                    }
                }
                );
                _$areaSelect.empty();
                _$areaSelect.select2({ data: items, theme: 'bootstrap4' });
            })
        } else {
            _$areaSelect.prop("disabled", true);
        }
    }
    var _districtService = abp.services.app.district,
        l = abp.localization.getSource('classifieds'),
        _$modal = $('#DistrictEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var district = _$form.serializeFormToObject();
        abp.ui.setBusy(_$form);
        _districtService.update(district).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('district.edited', district);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

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
