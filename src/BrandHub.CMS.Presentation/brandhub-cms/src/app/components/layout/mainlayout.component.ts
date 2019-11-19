import { Component, OnInit } from '@angular/core';
import { SidebarComponent } from './partial/sidebar.component';
import { TopnavComponent } from './partial/topnav.component';
import { } from 'jquery';
@Component({
  selector: 'app-mainlayout',
  templateUrl: './mainlayout.component.html',
  styleUrls: ['./mainlayout.component.css']
})
export class MainlayoutComponent implements OnInit {

  constructor() {}

  ngOnInit() {
  	$('body').addClass('nav-md');
  }

  ngAfterViewInit() {
    var $rightCol = $(".right_col");
    var $body = $('body');
    var $footer = $('footer');
    var $leftCol = $(".left_col");
    var $sideBarFooter = $('.sidebar-footer');
    var $navMenu = $(".nav_menu");
    $rightCol.css("min-height", $(window).height());
    var a = $body.outerHeight(),
      b = $body.hasClass("footer_fixed") ? -10 : $footer.height(),
      c = $leftCol.eq(1).height() + $sideBarFooter.height(),
      d = a < c ? c : a;
      d -= $navMenu.height() + b,
    $rightCol.css("min-height", d);

  }
}
