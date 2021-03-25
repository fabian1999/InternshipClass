$(document).ready(function () {

    $(".remove").click(function () {

        var listItem = $("i").parent().index();
        var className = $("i").parent().attr("id");

        console.log(listItem);
        console.log(className);

        $.ajax({
            method: "DELETE",
            url: `/Home/RemoveMember?index=${listItem}`,
            success: function (data) {
                // Remember string interpolation
                $("#" + className).remove();

                //$("#newcomer").val("");
            },
            error: function (data) {
                alert(`Failed to remove`);
            },
        });
    })

});