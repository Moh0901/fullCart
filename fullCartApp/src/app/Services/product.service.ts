import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  ProductAPIUrl = "https://localhost:7201/api/Inventory/";

  constructor(private http: HttpClient) { }

  getAllProducts(){
    return this.http.get(this.ProductAPIUrl+"getall");
  }

  getProductById(id:number){
    return this.http.get(this.ProductAPIUrl+"getby/"+id);
  }

  addNewProduct(product:any){
    return this.http.post(this.ProductAPIUrl+"addnew/",product);
  }

  updateProduct(id:number, product:any){
    return this.http.put(this.ProductAPIUrl+"updateproduct/"+id, product);
  }

  deleteProduct(id:number){
    return this.http.delete(this.ProductAPIUrl +"deleteproduct?id="+ id);
  }
}
