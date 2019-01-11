// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var pageSize = 6;
var pageIndex = 0;
var konec = false;

$(document).ready(function () {
    GetData();
    $(".objave").scroll(function () {
        if (($(window).scrollTop() ==
            $(document).height() - $(window).height())) {
            GetData();
        }
    });
});

$(".deletePost").click(function () {
    var id = $(this).attr("data-id");
    $.get("/cms/delete/" + id)
        .done(function (data) {
            $("#alertDeleted").show();
            $("tr#" + id).hide();
        });
});


function GetData() {
    $.ajax({
        type: 'GET',
        url: '/Home/GetData',
        data: { "pageindex": pageIndex, "pagesize": pageSize },
        dataType: 'json',
        success: function (data) {
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Imagepath && data[i].Imagepath.length > 3) {
                        slika = data[i].Imagepath.substring(1);
                    } else {
                        slika = "https://literative.com/wp-content/uploads/WHA_vince_Stars-862x539.jpg";
                    }

                    $(".objave").append(`
<div class="col-md-4" wfd-id="44">
                        <div class="card mb-4 shadow-sm" wfd-id="45">
                                <img class="card-img-top" data-src="holder.js/100px225?theme=thumb&amp;bg=55595c&amp;fg=eceeef&amp;text=Thumbnail" alt="Thumbnail [100%x225]" style="height: 225px; width: 100%; display: block;" src="`+ slika + `" data-holder-rendered="ifue">
                                <div class="card-body" wfd-id="46">
                                    <p class="card-text">`+ data[i].title + `</p>
                                    <div class="d-flex justify-content-between align-items-center" wfd-id="47">
                                        <div class="btn-group" wfd-id="48">
                                            <a href="/blog/post/`+ data[i].blogid + `" class="btn btn-sm btn-outline-secondary">View more</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>`);
                    /*if (i % 3 == 0) {
                        $("#novid").append("</div>");
                    }*/
                }
                pageIndex++;
            }
        },
        beforeSend: function () {
            $("#progress").show();
            $("#progress").addClass("text-center");
        },
        complete: function () {
            $("#progress").hide();
        },
        error: function () {
            alert("Error while retrieving data!");
        }
    });
}