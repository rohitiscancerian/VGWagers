$.validator.unobtrusive.adapters.add("agerangevalidation", ["minage", "maxage"], function (options) {
    options.rules["agerangevalidation"] = options.params;
    options.messages["agerangevalidation"] = options.message;
});

$.validator.addMethod("agerangevalidation",function(value,elements,params){
    if (value)
    {
        var valdate = new Date(value);
        if (
            (new Date().getFullYear - valdate.getFullYear()) < parseInt(params.minage) ||
            (new Date().getFullYear - valdate.getFullYear()) > parseInt(params.maxage)
            )
        {
            return false;
        }
    }
    return true;
});

$.validator.unobtrusive.adapters.addBool("mandatory", "required");


$(function () {

    $('input[type="date"]').attr('type', 'text');

    $(".datepicker").datetimepicker({
        format: 'DD/MM/YYYY',

        showClose: true,

        showClear: true,

        toolbarPlacement: 'bottom'
    });

    $('#registerform', this).submit(function (e) {
        e.preventDefault();
        if (!$(this).valid()) {
            return false;
        }
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            beforeSend: function () {
                $("#modal-loading-div-background").width($("#modalContent").width());
                $("#modal-loading-div-background").show();
            }
        }).done(function (returnedJSON) {
            $("#modal-loading-div-background").hide();
            if (returnedJSON.success) {
                $('#modal-container').modal('hide');
            }
            else {
                if (returnedJSON.msg != null) {
                    var alertBox = $("#alertBox");
                    alertBox.html(returnedJSON.msg);
                    alertBox.css("display", "block");
                }
                $.each(returnedJSON.errors, function (key, val) {
                    var container = $('span[data-valmsg-for="' + key + '"]');
                    container.removeClass("field-validation-valid").addClass("field-validation-error");
                    container.html(val[val.length - 1].ErrorMessage);
                });
                return false;
            }
        });
    });


    //allow validation framework to parse DOM
    $.validator.unobtrusive.parse('#registerform');
});