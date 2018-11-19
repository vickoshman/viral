var pageSize = 10;
var pageIndex = 0;

$(document).ready(function () {
  ko.applyBindings(new ViewModel());
  GetData();

  $(window).scroll(function () {
    if ($(window).scrollTop() ==
      $(document).height() - $(window).height()) {
      GetData();
    }
  });
});

function ViewModel() {
  posts = ko.observableArray([]);
  GetData();
}

function GetData() {
  $.ajax({
    type: 'GET',
    url: '/post/GetTop',
    data: { "pageindex": pageIndex, "pagesize": pageSize },
    dataType: 'json',
    success: function (data) {
      if (data != null) {
        for (var i = 0; i < data.length; i++) {
          var post = data[i];
          posts.push(post);

          /*$("#post-container").append("<article class=\"main-post\">                                                                                                                                                                                                                          " +
"            <div class=\"article-top\">                                                                                                                                                                                                                                                         " +
"              <h1>                                                                                                                                                                                                                                                                            " +
            "                <a href=\"post.html\">" + post.Title +"</a>                                                                                                                                                                                                                               " +
"              </h1>                                                                                                                                                                                                                                                                           " +
"              <hr />                                                                                                                                                                                                                                                                          " +
"              <div class=\"counters-line\">                                                                                                                                                                                                                                                     " +
"                <div class=\"pull-left\">                                                                                                                                                                                                                                                       " +
"                  <div class=\"date\">                                                                                                                                                                                                                                                          " +
"                    <i class=\"icon-date\"></i> 23.04                                                                                                                                                                                                                                           " +
"                        </div>                                                                                                                                                                                                                                                                " +
"                  <div class=\"user\">                                                                                                                                                                                                                                                          " +
"                    <i class=\"icon-user\"></i> <a href=\"profile.html\">johnjsu</a>                                                                                                                                                                                                              " +
"                  </div>                                                                                                                                                                                                                                                                      " +
"                  <div class=\"comments\">                                                                                                                                                                                                                                                      " +
"                    <i class=\"icon-comments\"></i> <a href=\"post.html\">320</a>                                                                                                                                                                                                                 " +
"                  </div>                                                                                                                                                                                                                                                                      " +
"                </div>                                                                                                                                                                                                                                                                        " +
"                <div class=\"pull-right\">                                                                                                                                                                                                                                                      " +
"                  <div class=\"like\">                                                                                                                                                                                                                                                          " +
"                    <a href=\"#\"><i class=\"icon-like\"></i> 56</a>                                                                                                                                                                                                                              " +
"                  </div>                                                                                                                                                                                                                                                                      " +
"                  <div class=\"dislike\">                                                                                                                                                                                                                                                       " +
"                    <a href=\"#\"><i class=\"icon-dislike\"></i> 32</a>                                                                                                                                                                                                                           " +
"                  </div>                                                                                                                                                                                                                                                                      " +
"                </div>                                                                                                                                                                                                                                                                        " +
"              </div>                                                                                                                                                                                                                                                                          " +
"              <div class=\"buttons-bar\">                                                                                                                                                                                                                                                       " +
"                <div class=\"buttons\">                                                                                                                                                                                                                                                         " +
"                  <a href=\"#\" class='repost has-tooltip' data-title=\"REPOST\">Repost</a>                                                                                                                                                                                                       " +
"                  <a href=\"#\" class='bookmarked has-tooltip' data-title=\"BOOKMARKED\">bookmarked</a>                                                                                                                                                                                           " +
"                  <div class=\"count\">2430</div>                                                                                                                                                                                                                                               " +
"                </div>                                                                                                                                                                                                                                                                        " +
"                <div class=\"social-icons\">                                                                                                                                                                                                                                                    " +
"                  <a href=\"javascript:void(0)\" data-href=\"https://www.facebook.com/sharer/sharer.php?u=http://teothemes.com/html/Aruna \" class='facebook has-tooltip' data-title=\"Share on Facebook\">facebook</a>                                                                              " +
"                  <a href=\"javascript:void(0)\" data-href=\"https://twitter.com/intent/tweet?source=tweetbutton&amp;text=Check the best image/meme sharing website!&url=http://teothemes.com/html/Aruna\" class='twitter has-tooltip' data-title=\"Share on Twitter\">twitter</a>                  " +
"                  <a href=\"javascript:void(0)\" data-href=\"https://plus.google.com/share?url=http://teothemes.com/html/Aruna\" class='googleplus has-tooltip' data-title=\"Share on Google +\">googleplus</a>                                                                                     " +
"                  <a href=\"mailto:?subject=Check out the best image/meme sharing template!&amp;body=Hey there! Check out the best image/meme sharing template! http://teothemes.com/html/Aruna\" class='mail has-tooltip' data-title=\"Share via Email\">mail</a>                                " +
"                </div>                                                                                                                                                                                                                                                                        " +
"              </div>                                                                                                                                                                                                                                                                          " +
"            </div>                                                                                                                                                                                                                                                                            " +
"            <div class=\"article-content\">                                                                                                                                                                                                                                                     " +
"              <figure>                                                                                                                                                                                                                                                                        " +
"            < div class=\"corner-tag green\" > " +
            "                  <a href=\"category.html\">Gingerfun</a>" +
            "                </div>" +
            "                <img class=\"lazy\" data-original=\"img/blog-post01.jpg\" alt=\"\" />" +
            "              </figure>" +
            "            </div>" +
"          </article>");*/
        }
        pageIndex++;
      }
    },
    beforeSend: function () {
      $("#progress").show();
    },
    complete: function () {
      $("#progress").hide();
    },
    error: function () {
      alert("Error while retrieving data!");
    }
  });
}