import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit {

  updateCategoryForm: FormGroup;
  // formData = new FormData();
  formData: any;
  imagePath = '';
   
  constructor(private categoryService: CategoryService, private router: Router, private fb: FormBuilder, private http: HttpClient, private activateRoute: ActivatedRoute) {
    //this.formData;
    this.updateCategoryForm = this.fb.group({
      categoryname: [''],
      userid: [''],
      imagepath: [''],
    });
  }

  id = this.activateRoute.snapshot.params['id'];
  ngOnInit(): void {
    this.categoryService.getCategoryById(this.activateRoute.snapshot.params['id'])
    .subscribe((res:any)=>{
      console.log(res); 
      this.imagePath = '';
      this.imagePath= res.ImagePath;
      this.updateCategoryForm = this.fb.group({
        categoryname: new FormControl(res["categoryName"]),
       // imagepath: new FormControl(res["imagePath"]),
         userid: new FormControl(res["userId"]),
         imagepath: new FormControl(res["imagePath"]),
      })
    });
  }

  // Handle the file input change event
  onFileChange(event: any) {
    //this.formData = [];
    // Get the selected file from the event
    const ImagePath = event.target.files[0] as File;
    // Update the form control value with the file name
    this.updateCategoryForm.patchValue({
      ImagePath: ImagePath.name
    });
    // Create a new FormData object and append the file to it
    this.formData = new FormData();
    this.formData.append('ImagePath', ImagePath, ImagePath.name);
  }

  categoryUpdated(id:number){
     // Get the other form values
     const name = this.updateCategoryForm.get('categoryname')?.value;
     const user = this.updateCategoryForm.get('userid')?.value;
     // Append them to the FormData object
     this.formData.append('CategoryName', name);
     this.formData.append('UserId', user);
     this.formData.append('CreatedAt', '');
     this.formData.append('UpdatedAt', '');
     // Make the HTTP post request to the Web API endpoint
     this.http.put("https://localhost:7201/api/Category/updatecategory?id="+id, this.formData, 
     ).subscribe(response => {
        // Handle the response
        console.log(response);
        this.router.navigate(["get-category"]);
      });
  }
}
