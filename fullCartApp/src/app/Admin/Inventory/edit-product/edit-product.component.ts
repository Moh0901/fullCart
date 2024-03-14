import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent {

  updateProductForm: FormGroup;
  // formData = new FormData();
  formData: any;

  constructor(private productService: ProductService, private router: Router, private fb: FormBuilder, private http: HttpClient, private activateRoute:ActivatedRoute) {
    //this.formData;
    this.updateProductForm = this.fb.group({
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

id = this.activateRoute.snapshot.params[ "id" ];

ngOnInit() {
  this.productService.getProductById(this.activateRoute.snapshot.params['id'])
  .subscribe((res:any)=>{
    this.updateProductForm = this.fb.group({
      name: new FormControl(res["name"]),
      imagepath: new FormControl(res["imagePath"]),
      userid: new FormControl(res["userId"]),
      description: new FormControl(res["description"]),
      price: new FormControl(res["price"]),
      brandid: new FormControl(res["brandId"]),
      categoryid: new FormControl(res["categoryId"]),
      quantity: new FormControl(res["quantity"]),
      status: new FormControl(res["status"])
    })
  });
}
   // Handle the file input change event
   onFileChange(event: any) {
    //this.formData = [];
    // Get the selected file from the event
    const ImagePath = event.target.files[0] as File;
    // Update the form control value with the file name
    this.updateProductForm.patchValue({
      ImagePath: ImagePath.name
    });
    // Create a new FormData object and append the file to it
    this.formData = new FormData();
    this.formData.append('ImagePath', ImagePath, ImagePath.name);
  }

  updateSubmitted(id:number){

  }
}
