$(document).ready(function () {
  $(".button-collapse").sideNav();
  $(".dropdown-button").dropdown({ hover: false });

  var url = window.location;
  $('nav').find('.active').removeClass('active');
  $('nav li a').each(function () {
    if (this.href == url) {
      $(this).parent().addClass('active');
    }
  });
});