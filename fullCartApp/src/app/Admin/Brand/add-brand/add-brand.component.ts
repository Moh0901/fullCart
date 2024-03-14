import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { BrandService } from 'src/app/Services/brand.service';

@Component({
  selector: 'app-add-brand',
  templateUrl: './add-brand.component.html',
  styleUrls: ['./add-brand.component.css']
})
export class AddBrandComponent {
  addBrandForm: FormGroup;
  // formData = new FormData();
  formData: any

  constructor(private brandService: BrandService, private router: Router, private fb: FormBuilder, private http: HttpClient) {
    //this.formData;
    this.addBrandForm = this.fb.group({
      brandname: [''],
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
    this.addBrandForm.patchValue({
      ImagePath: ImagePath.name
    });
    // Create a new FormData object and append the file to it
    this.formData = new FormData();
    this.formData.append('ImagePath', ImagePath, ImagePath.name);
  }

  // Handle the form submit event
  brandSubmitted() {
    // Get the other form values
    const name = this.addBrandForm.get('brandname')?.value;
    const user = this.addBrandForm.get('userid')?.value;
    // Append them to the FormData object
    this.formData.append('BrandName', name);
    this.formData.append('UserId', user);
    this.formData.append('CreatedAt', '');
    this.formData.append('UpdatedAt', '');
    // Make the HTTP post request to the Web API endpoint
    this.http.post('https://localhost:7201/api/Brand/addnew', this.formData)
      .subscribe(response => {
        // Handle the response
        console.log(response);
        this.addBrandForm.reset();
      });
  }
}
