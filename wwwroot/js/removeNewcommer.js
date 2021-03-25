$(document).ready(function () {

    $(".remove").click(function () {

        var listItem = $(this).parent('li').index();
        var className = $(this).parent('li').attr("id");

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
