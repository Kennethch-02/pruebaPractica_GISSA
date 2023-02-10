
$(document).ready(function () {
    _eventos();
    var selectedValues = [];
    $('.hability').change(function () {
        var value = $(this).val();
        if ($(this).is(':checked')) {
            if (selectedValues.indexOf(value) == -1) {
                selectedValues.push(value);
            }
        } else {
            selectedValues = selectedValues.filter(function (val) {
                return val !== value;
            });
        }
        var string = selectedValues.join(',');
        $('#habilitysInput').val(string);
    });

    $("#addNumberButton").click(function () {
        var phoneNumber = $("#showNumberInput").val();
        if (!/^\d+$/.test(phoneNumber)) {
            alert("Por favor ingrese solo números en el número de teléfono.");
            return;
        }
        var currentNumbers = $("#numbersInput").val();
        if (currentNumbers === undefined) {
            currentNumbers = "";
        } else if (currentNumbers.length > 0) {
            currentNumbers += ",";
        }
        currentNumbers += phoneNumber;
        $("#numbersInput").val(currentNumbers);
        $("#showNumberInput").val("");
    });
});
function _eventos() {
    $("#exampleModal").hide();
    $("#mostrarHabilidades").click(event => {
        $("#exampleModal").show();
    });
    $("#ocultarHabilidades").click(event => {
        $("#exampleModal").hide();
    });
    $("#verifyUsers").click(event => {
        if (!validateForm()) {
            return;
        }
        $("#registerUsers").click();
    });
}
function validateForm() {
    var identificacionId = $("#identificacionId").val();
    var selectNacionality = $("#selectNacionality option:selected").text();
    var nombreId = $("#nombreId").val();
    var nombreUsuarioId = $("#nombreUsuarioId").val();
    var claveId = $("#claveId").val();
    var emailID = $("#emailID").val();
    var habilitysInput = $('#habilitysInput').val();
    var currentNumbers = $("#numbersInput").val();
    // 1. Validar identificacionId
    if (!/^\d+$/.test(identificacionId)) {
        alert("El campo identificacion solo debe contener números.");
        return false;
    }
    if (selectNacionality === "Nacional" && identificacionId.length !== 9) {
        alert("Si la nacionalidad seleccionada es 'Nacional', el campo 'Identificacion' debe tener 9 caracteres.");
        return false;
    }

    // 2. Validar nombreId
    if (!nombreId) {
        alert("El campo de nombre completo no puede estar vacío.");
        return false;
    }

    // 3. Validar nombreUsuarioId
    if (!nombreUsuarioId) {
        alert("El campo nombre de usuario no puede estar vacío.");
        return false;
    }

    // 4. Validar claveId
    if (!/^(?=.*\d)(?=.*[a-zA-Z]).{6,}$/.test(claveId)) {
        alert("El campo contraseña debe tener al menos 6 caracteres y combinar letras y números.");
        return false;
    }

    // 5. Validar emailID
    if (!/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(emailID)) {
        alert("El campo email debe ser un correo electrónico válido.");
        return false;
    }

    // 6. Validar numeros de telefono
    if (currentNumbers == "") {
        alert("Debe agregar almenos un numero de telefono.");
        return false;
    }

    // 7. Validar las habilidades
    if (habilitysInput.split(",") < 3) {
        alert("Debe seleccionar almenos 3 habilidades.");
        return false;
    }
    
    return true;
}