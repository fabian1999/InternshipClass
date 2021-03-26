$(document).ready(function () {

    $("#list").on("click", ".remove", function () {

        $("#list").append(`<li class="member"><span class="name">${data}</span><span class="delete fa fa-remove"></span><i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
		        </li>`);

        $("#newcomer").val("");

        $("#list").on("click", ".startEdit", function () {
            var targetMemberTag = $(this).closest('li');
            var index = targetMemberTag.index(targetMemberTag.parent());
            var currentName = targetMemberTag.find(".name").text();
            $('#editClassmate').attr("memberIndex", index);
            $('#classmateName').val(currentName);
        })

        $("#editClassmate").on("click", "#submit", function () {
            console.log('submit changes to server');
        })

        $("#editClassmate").on("click", "#cancel", function () {
            console.log('cancel changes');
        })
    })

});
