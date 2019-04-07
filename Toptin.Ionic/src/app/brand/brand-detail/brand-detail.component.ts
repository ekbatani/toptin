import { Component, OnInit, Input } from '@angular/core';
import { Brand } from 'src/app/_models/brand';

@Component({
  selector: 'app-brand-detail',
  templateUrl: './brand-detail.component.html',
  styleUrls: ['./brand-detail.component.css']
})
export class BrandDetailComponent implements OnInit {

  @Input() brand: Brand;
  constructor() { }

  ngOnInit() {
    console.log(this.brand);
  }

}
