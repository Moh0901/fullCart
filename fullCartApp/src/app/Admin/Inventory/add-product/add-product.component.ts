import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  addProductForm: FormGroup;
  // formData = new FormData();
  formData: any;
  excelFile: File | null = null;

  constructor(private productService: ProductService, private router: Router, private fb: FormBuilder, private http: HttpClient) {
    //this.formData;
    this.addProductForm = this.fb.group({
      name: [''],
      imagepath: [''],
      userid: [''],
      description: [''],
      price: [''],
      brandid: [''],
      categoryid: [''],
      quantity: [''],
      status: ['']
    });
  }

  // Handle the file input change event
  onFileChange(event: any) {
    //this.formData = [];
    // Get the selected file from the event
    const ImagePath = event.target.files[0] as File;
    // Update the form control value with the file name
    this.addProductForm.patchValue({
      ImagePath: ImagePath.name
    });
    // Create a new FormData object and append the file to it
    this.formData = new FormData();
    this.formData.append('ImagePath', ImagePath, ImagePath.name);
  }

  // Handle the form submit event
  productSubmitted() {
    // Get the other form values
    const name = this.addProductForm.get('name')?.value;
    const user = this.addProductForm.get('userid')?.value;
    const description = this.addProductForm.get('description')?.value;
    const price = this.addProductForm.get('price')?.value;
    const brand = this.addProductForm.get('brandid')?.value;
    const category = this.addProductForm.get('categoryid')?.value;
    const quantity = this.addProductForm.get('quantity')?.value;
    const status = this.addProductForm.get('status')?.value;

    // Append them to the FormData object in text values that are keys
    this.formData.append('Name', name);
    this.formData.append('UserId', user);
    this.formData.append('Description', description);
    this.formData.append('Price', price);
    this.formData.append('BrandId', brand);
    this.formData.append('CategoryId', category);
    this.formData.append('Quantity', quantity);
    this.formData.append('Status', status);
    this.formData.append('CreatedAt', '');
    this.formData.append('UpdatedAt', '');
    // Make the HTTP post request to the Web API endpoint
    this.http.post('https://localhost:7201/api/Inventory/addnew', this.formData)
      .subscribe(response => {
        // Handle the response
        console.log(response);
        this.addProductForm.reset();
      });
  }
  handleUpload(): void {
    if (this.excelFile) {
      const formData = new FormData();
      formData.append('formFile', this.excelFile);

      // Adjust the API endpoint as needed
      this.http.post('https://localhost:7201/api/ExcelProducts/upload', formData).subscribe(
        (response) => {
          // Handle successful upload response
          console.log('File uploaded successfully:', response);
        },
        (error) => {
          // Handle upload error
          console.error('Error uploading file:', error);
        }
      );
    }
  }

  onFile(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files && inputElement.files.length > 0) {
      this.excelFile = inputElement.files[0];
    }
  }
} 



