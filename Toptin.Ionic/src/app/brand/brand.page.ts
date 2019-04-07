import { Component, OnInit } from '@angular/core';
import { BrandService } from '../_services/brand.service';
import { Brand } from '../_models/brand';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.page.html',
  styleUrls: ['./brand.page.scss'],
})
export class BrandPage implements OnInit {

  brands: Brand[];

  constructor(private brandService: BrandService) { }

  ngOnInit() {
    this.getBrands();
  }

  getBrands() {
    this.brandService.getBrands().subscribe((brands: Brand[]) => {
      this.brands = brands;
    }, error => {
      console.log(error);
    });
  }

}
