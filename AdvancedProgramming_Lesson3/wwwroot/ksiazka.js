const uri = "api/ksiazkaitems";
let ksiazka = null;
function getCount(data) {
    const el = $("#counter");
    let title = "title";
    if (data) {
        if (data > 1) {
            ksiazka = "ksiazka";
        }
        //el.text(data + " " + title);
    } else {
        //el.text("No " + title);
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
            const tBody = $("#ksiazka");
            $(tBody).empty();
            getCount(data.length);
            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.title))
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
            ksiazka = data;
        }
    });
}
function addItem() {
    const item = {
        title: $("#add-title").val(),
        isComplete: false
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
            $("#add-title").val("");
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
    $.each(ksiazka, function (key, item) {
        if (item.id === id) {
            $("#edit-title").val(item.title);
            $("#edit-id").val(item.id);
        }
    });
    $("#spoiler").css({ display: "block" });
}
$(".my-form").on("submit", function () {
    const item = {
        title: $("#edit-title").val(),
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