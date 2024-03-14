import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  categoryAPIUrl = "https://localhost:7201/api/Category/";

  constructor(private http: HttpClient) { }

  getAllCategory(){
    return this.http.get(this.categoryAPIUrl+"getall");
  }

  getCategoryById(id:number){
    return this.http.get(this.categoryAPIUrl+"getby/"+id);
  }

  addNewCategory(category:any){
    return this.http.post(this.categoryAPIUrl+"AddNew/",category);
  }

  updateCategory(id:number, category:any){
    return this.http.put(this.categoryAPIUrl+"updatecategory/"+id, category);
  }

  deleteCategory(id:number){
    return this.http.delete(this.categoryAPIUrl+"deletecategory?id="+id);
  }

}
