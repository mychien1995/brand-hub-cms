import { Directive, ElementRef } from '@angular/core';
import { Router } from '@angular/router';

@Directive({
  selector: '[router-link]'
})
export class RouterLinkDirective {
  constructor(private el: ElementRef, private router: Router) {

  }

  ngOnInit(): void {
    var el = this.el;
    var router = this.router;
    setTimeout(function() {
      var href = el.nativeElement.getAttribute('href');
      el.nativeElement.addEventListener('click', function(e) {
        e.preventDefault();
        router.navigate([href]);
      });
      el.nativeElement.setAttribute('href', 'javascript:void(0)');
    });
  }
}
