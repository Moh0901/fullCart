import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/Services/product.service';
import * as saveAs from 'file-saver';

@Component({
  selector: 'app-get-product',
  templateUrl: './get-product.component.html',
  styleUrls: ['./get-product.component.css']
})
export class GetProductComponent implements OnInit{

  brand: any = [];
  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct(){
      this.productService.getAllProducts().subscribe((data)=>{
        console.log(data);
        this.brand=data;
      })
    }

    deleteProduct(id: number){
      this.productService.deleteProduct(id).subscribe(() =>{
        this.getProduct();
      });
      alert("Delete Successfully");
    }
    downloadProductsInExcel() {
      this.productService.exportToExcel().subscribe(
        (blob: Blob) => {
          saveAs(blob, 'productsList.xlsx');
        },
        (error) => {
          console.error(error);
        }
      );
    }
}


