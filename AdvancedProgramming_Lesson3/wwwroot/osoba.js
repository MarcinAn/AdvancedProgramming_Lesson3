const uri = "api/osobaitems";
let osoba = null;
function getCount(data) {
    const el = $("#counter");
    let name = "name";
    let lastname = "lastname";
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
        url: uri + '/CreateOsobaItem',
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

function updateItem() {
    var id = parseInt($("#edit-id").val(), 10);
    const item = {
        id: id,
        name: $("#edit-name").val(),
        lastname: $("#edit-lastname").val(),
    };
    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri + '/UpdateOsobaItem',
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
            closeInput();
        }
    });
}

function closeInput() {
    $("#spoiler").css({ display: "none" });
}