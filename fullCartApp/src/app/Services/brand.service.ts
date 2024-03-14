import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  BrandAPIUrl = "https://localhost:7201/api/Brand/";

  constructor(private http: HttpClient) { }

  getAllBrand(){
    return this.http.get(this.BrandAPIUrl+"allbrands");
  }

  getBrandById(id:number){
    return this.http.get(this.BrandAPIUrl+"getby/"+id);
  }

  addNewBrand(brand:any){
    return this.http.post(this.BrandAPIUrl+"addnew/",brand);
  }

  updateBrand(id:number, brand:any){
    return this.http.put(this.BrandAPIUrl+"updatebrand/"+id, brand);
  }

  deleteBrand(id:number){
    return this.http.delete(this.BrandAPIUrl +"deletecategory?id="+ id);
  }
}
