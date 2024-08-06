
var livro = {
    start: function () {
        $('#Preco').mask("#.##0,00", { reverse: true });

        var autores = document.getElementById('AutoresSelecionados');
        new Choices(autores, {
            delimiter: ',',
            editItems: true,
            removeItemButton: true,
            addItems: true
        });

        var assuntos = document.getElementById('AssuntosSelecionados');
        new Choices(assuntos, {
            delimiter: ',',
            editItems: true,
            removeItemButton: true,
            addItems: true
        });

        setTimeout(() => {
            $(".choices__input--cloned").attr("placeholder", "Pesquisar");
        }, 1000);
    }
}

$(function () {
    livro.start();
});