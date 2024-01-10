var blogs = {
    init: function () {
        $('#btnComment').click(function () {
            $.ajax({
                url: '/Blogs/SendComment',
                method: 'post',
                dataType:'json',
                data: {
                    comment: $('#ipComment').val(),
                    idBlog: $('#ipIdBlogHidden').val(),
                },
                success: function (res) {
                    if (res.status) {
                        base.success(res.message);
                        setTimeout(function () {
                            location.reload();
                            var scrollPosition = $('#comment-template').scrollTop();
                            $(window).on('beforeunload', function () {
                                $(window).scrollTop(scrollPosition);
                            });
                        }, 1500);
                    }
                    else {
                        base.error(res.message);
                    }
                }
            });
        });
    }
}
$(document).ready(function () {
  
    blogs.init();
});