import { Component, OnInit } from '@angular/core';
import { BrandService } from '../_services/brand.service';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.page.html',
  styleUrls: ['./brand.page.scss'],
})
export class BrandPage implements OnInit {

  brands: any = {};

  constructor(private brandService: BrandService) { }

  ngOnInit() {
    this.getBrands();
  }

  getBrands() {
    this.brandService.getBrands().subscribe((brands) => {
      this.brands = brands;
      console.log(this.brands);
    }, error => {
      console.log(error);
    });
  }

}
