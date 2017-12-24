$(document).ready(function () {
    
    fn_Listar();
    $("#actualizar").prop("disabled", true);
});
 
function fn_Listar()
{
    if (fn_Select()) {

        $.getJSON('services/Productos/Listar', function (data) {
            $('#productos').html('');
            $.each(data, function (p, val) {
                var item = '';
                item += '<table border =1 style=width:500px ><tr>';
                item += '<td align=center>' + val.idProducto + '</td>';
                item += '<td align=center>' + val.Nombre + '</td>';
                item += '<td align=center>' + val.Precio + '</td>';
                item += '<td align=center>' + val.Stock + '</td>';
                item += '<td align=center>' + val.NombreCategoria + '</td>';
                //item += '<td align=center> <input type=button id="Actualizar" value="Buscar" onclick="Buscar(' + val.idProducto + ')";/> </td>';
                //item += '<td align=center> <input type=button id="Eliminar" value="Eliminar" name="'+ val.idProducto+'";/> </td>';
                item += '</tr></table>';
                $('#productos').append(item);
            });
        });
    }
    else
    {
        $.getJSON('services/Productos/ListarXML', function (data) {
            $('#productos').html('');
            $.each(data, function (p, val) {
                var item = '';
                item += '<table border =1 style=width:500px ><tr>';
                item += '<td align=center>' + val.idProducto + '</td>';
                item += '<td align=center>' + val.Nombre + '</td>';
                item += '<td align=center>' + val.Precio + '</td>';
                item += '<td align=center>' + val.Stock + '</td>';
                item += '<td align=center>' + val.NombreCategoria + '</td>';
                //item += '<td align=center> <input type=button id="Actualizar" value="Buscar" onclick="Buscar(' + val.idProducto + ')";/> </td>';
                //item += '<td align=center> <input type=button id="Eliminar" value="Eliminar" name="'+ val.idProducto+'";/> </td>';
                item += '</tr></table>';
                $('#productos').append(item);
            });
        });
    }
}

function crear(nuevo, callback)
{
    if (fn_Select()) {
        $.ajax({
            url: "services/productos/post",
            data: JSON.stringify(nuevo),
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            statusCode: {
                201: function (producto) {
                    callback(producto);
                },
            }
        });
    }
    else 
    {
        $.ajax({
            url: "services/productos/XML",
            data: JSON.stringify(nuevo),
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            statusCode: {
                201: function (producto) {
                    callback(producto);
                },
            }
        });
    }
}

$('#crear').live({
    click: function () {
        var sNombre = $('#txt_nombre').val();
        var sPrecio = $('#txt_precio').val();
        var sStock = $('#txt_stock').val();
        if (sNombre != "" && sPrecio != "" && sStock != "") 
        {
            var nuevo = {
                nombre: $('#txt_nombre').val(),
                precio : $('#txt_precio').val(),
                stock: $('#txt_stock').val(),
                idCategoria: $('#ddl_categoria').val(),
            };

            crear(nuevo, function (producto) {
                alert("Se creo el producto " + nuevo.nombre);
                fn_Listar();
            });
        } else {
            alert("Todos los campos son obligatorios.");
        }
    }
});

function buscar(id, callback) {
    var nuevo = {
        idProducto: id
    };
    $.ajax({
        url: "services/productos/Buscar",
        // data: {id:id},
        data: JSON.stringify(nuevo),
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        statusCode: {
            200: function (producto) {
                callback(producto)
            },
            404: function () {
                alert("Producto no encontrado")
            },
        }
    });
}

$('#buscar').live({    
    click: function () {
        var id = $('#txt_cod').val();        
        buscar(id, function (producto)
        {
            try{
                $('#txt_nombre').val(producto[0].Nombre);
                $('#txt_precio').val(producto[0].Precio);
                $('#txt_stock').val(producto[0].Stock);
                $('#ddl_categoria').val(producto[0].idCategoria);
                $("#actualizar").removeAttr('disabled');
                $("#crear").prop("disabled", true);
            }
            catch (err) {
                alert("Producto no encontrado");
                $('#txt_nombre').val("");
                $('#txt_precio').val("");
                $('#txt_stock').val("");
                $("#crear").removeAttr('disabled');
                $("#actualizar").prop("disabled", true);
            }
        });
    }
});


function actualizar(nuevo, callback)
{   
    $.ajax({
        url: "services/productos/Put",
        data: JSON.stringify(nuevo),
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        statusCode: {
            201: function () {
                callback();
            },
            404: function () {
                alert("Producto no encontrado");
            },
        }
    });  
}

$('#actualizar').live({
    click: function () {
        var nuevo = {
            nombre: $('#txt_nombre').val(),
            precio: $('#txt_precio').val(),
            stock: $('#txt_stock').val(),
            idCategoria: $('#ddl_categoria').val(),
        };
        actualizar(nuevo, function () {
            alert("Se actualizo el producto " + nuevo.nombre);
            $('#txt_cod').val("");
            $('#txt_nombre').val("");
            $('#txt_precio').val("");
            $('#txt_stock').val("");
            $("#crear").removeAttr('disabled');
            $("#actualizar").prop("disabled", true);
            fn_Listar();
        });     
    }
});

function eliminar(nuevo, callback) {
    $.ajax({
        url: "services/productos/Delete",
        data: JSON.stringify(nuevo),
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        statusCode: {
            201: function () {
                callback();
            },
            404: function () {
                alert("Producto no eliminado");
            },
        }
    });
}

$('#eliminar').live({
    click: function () {
        var nuevo = {
            idProducto: $('#txt_cod_e').val()
           
        };
        eliminar(nuevo, function () {
            alert("El producto se ha eliminado");          
            fn_Listar();
        });
    }
});

function fn_Select()
{
    if ($("#bd").is(':checked'))
        return true;
    else
        return false;

}

function fn_Check()
{
    fn_Listar();
}