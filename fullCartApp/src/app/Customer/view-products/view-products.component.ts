import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ProductService } from 'src/app/Services/product.service';

@Component({
  selector: 'app-view-products',
  templateUrl: './view-products.component.html',
  styleUrls: ['./view-products.component.css']
})

export class ViewProductsComponent implements  OnInit {

  products: any=[];
  searchText='';
  constructor(private productService: ProductService) { }

  ngOnInit(){
    this.getPorducts(); 
  }

  getPorducts() {
    this.productService.getAllProducts().subscribe((data) => {
      console.log("Data Received", data);
      this.products=data;
    });
  }
}
