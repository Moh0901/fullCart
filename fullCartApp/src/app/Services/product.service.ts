import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as XLSX from 'xlsx';

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

  exportToExcel(): Observable<Blob> {
    return this.http.get('https://localhost:7201/api/ExcelProducts/download', {
      responseType: 'blob'
    });
  }

  UploadExcel(formData: FormData) {  
    let headers = new HttpHeaders();  
  
    headers.append('Content-Type', 'multipart/form-data');  
    headers.append('Accept', 'application/json');  
  
    const httpOptions = { headers: headers };  
  
    return this.http.post("https://localhost:7201/api/ExcelProducts/upload", formData, httpOptions)  
  }  

}
