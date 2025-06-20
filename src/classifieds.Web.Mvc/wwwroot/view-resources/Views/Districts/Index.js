﻿(function ($) {
    var _cityService = abp.services.app.district,
        l = abp.localization.getSource('classifieds'),
        _$modal = $('#DistrictCreateModal'),
        _$form = _$modal.find('form'),
        _$areaSelect = $("#selectArea");
        _$table = $('#DistrictTable');

    var _$rolesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = $('#RolesSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _cityService.getAll(filter).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            }).always(function () {
                abp.ui.clearBusy(_$table);
            });
        },
        language: {
            "lengthMenu": "_MENU_ " + l("RecordPerPage"),
            "zeroRecords": "Nothing found - sorry",
            "infoEmpty": l("No records available"),
            "infoFiltered": "(filtered from _MAX_ total records)"
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$rolesTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'name',
                sortable: false
            },
            {
                targets: 2,
                data: 'area.city.name',
                sortable: false
            },
            {
                targets: 3,
                sortable: false,
                render: (data, type, row, meta) => {
                    if (row.area != null ){
                        return row.area.name;
                    }
                    return "";
                }

            },
            {
                targets: 4,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-district" data-id="${row.id}" data-toggle="modal" data-target="#DistrictEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-district" data-district-id="${row.id}" data-district-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var city = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _cityService
            .create(city)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                _$areaSelect.empty();
                _$areaSelect.select2('val', 'All');
                abp.notify.info(l('SavedSuccessfully'));
                _$rolesTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-district', function () {
        var Id = $(this).attr("data-district-id");
        var Name = $(this).attr('data-district-name');

        deleteRole(Id, Name);
    });

    $(document).on('click', '.edit-district', function (e) {
        var Id = $(this).attr("data-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'admin/districts/Edit/' + Id,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#DistrictEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        })
    });

    abp.event.on('district.edited', (data) => {
        _$rolesTable.ajax.reload();
    });

    function deleteRole(cityId, cityName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                cityName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _cityService.delete({
                        id: cityId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$rolesTable.ajax.reload();
                    });
                }
            }
        );
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$rolesTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$rolesTable.ajax.reload();
            return false;
        }
    });
    $('.select2').select2({ theme: 'bootstrap4' });
    var defaultCity = $('#selectCity').find(":selected").val();
    console.log(defaultCity);
    getAreas(Number(defaultCity));
    $('#selectCity').on('change', function (e) {
            getAreas(e.target.value);
    });

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

})(jQuery);
