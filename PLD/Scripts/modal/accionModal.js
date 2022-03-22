"use strict";


$('#modal-confirmar').on('show.bs.modal', function (event) {


    var button = $(event.relatedTarget); //Botón que desencadena el modal
    var url = button.attr("href");
    var modal = $(this)

    //Este remplace el contenido del  modal-content cada vez que es abierto
    modal.find('.modal-content').load(url);
});


$('#modal-confirmar').on('hidden.bs.modal', function (event) {

    //Elimina el atributo data  bs.modal de el.
    $(this).removeData('bs.modal');

    // y vacia el elemento modal-content
    $('#modal-confirmar .modal-content').empty();

   


});

