$(document).ready(function () {
    var selectedLanguage;
    var durationSelect;
    var sortSelect;

    var search = {
        tag: "",
        input: "",
        duration: "",
        sortBy: "",
    };

    $("#search2").on("click", function () {
        $(".jumbotron").slideUp("slow");
        search.tag = $('.selectpicker').selectpicker('val');
        search.input = $('.searchInput').val();
        search.duration = $('#selectDuration').html();
        search.sortBy = $("#sortBy").html();

        $.get(
           "/Home/Search",
           search, // put your parameters here
           function (data) {
               $('.videoContainer').html(data);
           }
        );

    });

    $("#languageSelect li a").on("click", function () {
        selectedLanguage = $(this).html();
        $("#selectedLanguage").html(selectedLanguage);
    })

    $("#durationSelect li a").on("click", function () {
        durationSelect = $(this).children().html();
        $("#selectDuration").html(durationSelect);
    })

    $("#sortSelect li a").on("click", function () {
        sortSelect = $(this).html();
        $("#sortBy").html(sortSelect);
    })
})