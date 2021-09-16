const uri = "api/osobaitems";
let name = null;
let lastname = null;
function getCount(data) {
    const el = $("#counter");
    let name = "name";
    let lastname = "lastname";
    if (data) {
        if (data > 1) {
            name = "name";
            lastname = "lastname";
        }
        //el.text(data + " " + name + " " + lastname);
    } else {
        //el.text("No " + name + lastname);
    }
}
$(document).ready(function () {
    getData();
});
function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#osoba");
            $(tBody).empty();
            getCount(data.length);
            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text("Pozycja " + item.id + ":"))
                    .append($("<td></td>").text(item.name))
                    .append($("<td></td>").text(item.lastName))
                    .append(
                        $("<td></td>").append(
                            $("<button>Edycja</button>").on("click", function () {
                                editItem(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Usuń</button>").on("click", function () {
                                deleteItem(item.id);
                            })
                        )
                    );
                tr.appendTo(tBody);
            });
            osoba = data;
        }
    });
}
function addItem() {
    const item = {
        name: $("#add-name").val(),
        lastname: $("#add-lastname").val(),
    };
    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
            $("#add-name").val("");
            $("#add-lastname").val("");
        }
    });
}
function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}
function editItem(id) {
    $.each(osoba, function (key, item) {
        if (item.id === id) {
            $("#edit-name").val(item.name);
            $("#edit-id").val(item.id);
            $("#edit-lastname").val(item.lastName);
        }
    });
    $("#spoiler").css({ display: "block" });
}
$(".my-form").on("submit", function () {
    const item = {
        name: $("#edit-name").val(),
        lastName: $("#edit-lastname").val(),
        id: $("#edit-id").val()
    };
    $.ajax({
        url: uri + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });
    closeInput();
    return false;
});
function closeInput() {
    $("#spoiler").css({ display: "none" });
}