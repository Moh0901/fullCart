import { Component, OnInit } from '@angular/core';
import { CategoryService } from './Services/category.service';
import { BrandService } from './Services/brand.service';
import { ProductService } from './Services/product.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'fullCartApp';

  // constructor(private category: CategoryService, private brand: BrandService, private product:ProductService){}
  // ngOnInit(): void {
  //   this.getcategory();
  //   this.getbrand();
  //   this.geproduct();
  // }

  
  // getcategory(){
  //   this.category.getAllCategory().subscribe((data)=>{
  //     console.log(data);
  //   })
  // }
  // getbrand(){
  //   this.brand.getAllBrand().subscribe((data)=>{
  //     console.log(data);
  //   })
  // }

  // geproduct(){
  //   this.product.getAllBrand().subscribe((data)=>{
  //     console.log(data);
  //   })
  // }
}
