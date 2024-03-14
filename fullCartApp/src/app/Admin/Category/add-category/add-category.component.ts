import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent {

  addCategoryForm: FormGroup;
  // formData = new FormData();
  formData: any

  constructor(private categoryService: CategoryService, private router: Router, private fb: FormBuilder, private http: HttpClient) {
    //this.formData;
    this.addCategoryForm = this.fb.group({
      categoryname: [''],
      imagepath: [''],
      userid: [''],

    });
  }

  // Handle the file input change event
  onFileChange(event: any) {
    //this.formData = [];
    // Get the selected file from the event
    const ImagePath = event.target.files[0] as File;
    // Update the form control value with the file name
    this.addCategoryForm.patchValue({
      ImagePath: ImagePath.name
    });
    // Create a new FormData object and append the file to it
    this.formData = new FormData();
    this.formData.append('ImagePath', ImagePath, ImagePath.name);
  }

  // Handle the form submit event
  categorySubmitted() {
    // Get the other form values
    const name = this.addCategoryForm.get('categoryname')?.value;
    const user = this.addCategoryForm.get('userid')?.value;
    // Append them to the FormData object
    this.formData.append('CategoryName', name);
    this.formData.append('UserId', user);
    this.formData.append('CreatedAt', '');
    this.formData.append('UpdatedAt', '');
    // Make the HTTP post request to the Web API endpoint
    this.http.post('https://localhost:7201/api/Category/AddNew', this.formData)
      .subscribe(response => {
        // Handle the response
        console.log(response);
        this.addCategoryForm.reset();
      });
  }
}