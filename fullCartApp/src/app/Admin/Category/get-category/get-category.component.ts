import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-get-category',
  templateUrl: './get-category.component.html',
  styleUrls: ['./get-category.component.css']
})
export class GetCategoryComponent implements OnInit{

  category: any = [];
  constructor(private categoryService:CategoryService) { }

  ngOnInit(): void {
    this.getcategory();
  }

  getcategory(){
      this.categoryService.getAllCategory().subscribe((data)=>{
        console.log(data);
        this.category=data;
      })
    }

    deleteCategory(id: number){
      this.categoryService.deleteCategory(id).subscribe(() =>{
        this.getcategory();
      });
      alert("Delete Successfully");
    }

}
