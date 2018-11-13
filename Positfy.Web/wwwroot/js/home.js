var loadMoreCounter = 0;

function IndexViewModel() {
  var self = this;
  self.Posts = ko.observableArray([]);

  self.NewPost = ko.observable("");

  self.TextCounter = ko.computed(function () {
    const newPost = self.NewPost();
    const remaining = 300 - newPost.length;
    return remaining;
  }); 

  self.GetPostDetails = function () {
    $.ajax({
      type: "POST",
      url: "Home/LoadMore",
      data: { "pageindex": loadMoreCounter },
      dataType: "json",
      success: function (response) {
        if (response !== null) {
          self.Posts($.map(response,
            function (post) {
              return new PostViewModel(post);
            }));
          $("button.like-disabled").attr("disabled", true);
        }
      },
      failure: function (response) {
        alert("Error while retrieving data!");
      }
    });
  };

  self.LoadMore = function () {
    loadMoreCounter++;
    $.ajax({
      type: "POST",
      url: "Home/LoadMore",
      data: { "loadMoreCounter": loadMoreCounter },
      dataType: "json",
      success: function (response) {
        if (response !== null && response !== "") {
          for (let i = 0; i < response.length; i++) {
            self.Posts.push(new PostViewModel(response[i]));
          }
          $("button.like-disabled").attr("disabled", true);
        } else {
          loadMoreCounter--;
        }
      },
      failure: function (response) {
        alert("Error while retrieving data!");
      }
    });
  };


  self.CreatePost = function () {
    $.ajax({
      type: "POST",
      url: "Home/CreatePost",
      data: { "Content": self.NewPost(), "Index": loadMoreCounter },
      dataType: "json",
      success: function (response) {
        if (response !== null) {
          self.Posts($.map(response,
            function (post) {
              return new PostViewModel(post);
            }));
        }
      },
      failure: function (response) {
        alert("Error while retrieving data!");
      }
    });
  };

  self.GetPostDetails();
}

function PostViewModel(data) {
  var self = this;
  self.Owner = ko.observable(data.owner);
  self.Content = ko.observable(data.content);
  self.PostDate = ko.observable(data.postDate);
  self.LikeCount = ko.observable(data.likeCount);
  self.NewComment = ko.observable("");
  self.Comments = ko.observableArray([]);
  self.TextCounter = ko.computed(function () {
    const newComment = self.NewComment();
    const remaining = 300 - newComment.length;
    return remaining;
  });

  
  if (data.isLikeable) {
    self.IsPostLikeable = ko.observable("");
  } else {
    self.IsPostLikeable = ko.observable("like-disabled");
  }

  self.LikeCountVisible = ko.computed(function () {
    if (self.LikeCount() > 0) {
      return true;
    } else {
      return false;
    }
  });

  self.Comments($.map(data.comments,
    function (comment) {
      return new CommentViewModel(comment);
    }));

  self.LikePost = function (item, event) {
    const context = ko.contextFor(event.target);
    const postId = context.$index();
    $.ajax({
      type: "POST",
      url: "Home/LikePost",
      data: { "postId": postId },
      dataType: "json",
      success: function (response) {
        if (response !== null) {
          self.LikeCount(response);
          self.IsPostLikeable("like-disabled");
          $("button.like-disabled").attr("disabled", true);
        }
      },
      failure: function (response) {
        alert("Error while retrieving data!");
      }
    });
  };

  self.CreateComment = function (item, event) {
    const context = ko.contextFor(event.target);
    const postId = context.$index();
    $.ajax({
      type: "POST",
      url: "Home/CreateComment",
      data: { "PostId": postId, "Content": self.NewComment() },
      dataType: "json",
      success: function (response) {
        if (response !== null && response !== "") {
          self.Comments($.map(response,
            function (comment) {
              return new CommentViewModel(comment);
            }));
          self.NewComment("");
        }
      },
      failure: function (response) {
        alert("Error while retrieving data!");
      }
    });
  };
}

function CommentViewModel(data) {
  var self = this;
  self.CommentOwner = ko.observable(data.user);
  self.CommentPostDate = ko.observable(data.published);
  self.CommentContent = ko.observable(data.content);
  self.CommentLikeCount = ko.observable(data.likeCount);

  if (data.isLikeable)
    self.IsCommentLikeable = ko.observable("");
  else
    self.IsCommentLikeable = ko.observable("like-disabled");

  self.LikeCountVisible = ko.computed(function () {
    if (self.CommentLikeCount() > 0) {
      return true;
    } else {
      return false;
    }
  });

  self.LikeComment = function (item, event) {
    const context = ko.contextFor(event.target);
    const commentId = context.$index();
    const postId = context.$parentContext.$index();
    $.ajax({
      type: "POST",
      url: "Home/LikeComment",
      data: { "CommentId": commentId, "PostId": postId },
      dataType: "json",
      success: function (response) {
        if (response !== null) {
          self.CommentLikeCount(response);
          self.IsCommentLikeable("like-disabled");
          $("button.like-disabled").attr("disabled", true);
        }
      },
      failure: function (response) {
        alert("Error while retrieving data!");
      }
    });
  };
}


ko.bindingHandlers.wordCountLimiter = {
  update: function (element, valueAccessor, allBindingsAccessor) {
    if (allBindingsAccessor().value()) {
      allBindingsAccessor().value(allBindingsAccessor().value().substr(0, valueAccessor()));
    }
  }
};

ko.applyBindings(new IndexViewModel());
