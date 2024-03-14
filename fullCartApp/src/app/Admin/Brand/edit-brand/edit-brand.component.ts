import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BrandService } from 'src/app/Services/brand.service';

@Component({
  selector: 'app-edit-brand',
  templateUrl: './edit-brand.component.html',
  styleUrls: ['./edit-brand.component.css']
})
export class EditBrandComponent {
  updateBrandForm: FormGroup;
  // formData = new FormData();
  formData: any
   
  constructor(private brandService: BrandService, private router: Router, private fb: FormBuilder, private http: HttpClient, private activateRoute: ActivatedRoute) {
    //this.formData;
    this.updateBrandForm = this.fb.group({
      brandname: [''],
      imagepath: [''],
      userid: [''],
    });
  }

  id = this.activateRoute.snapshot.params[ "id" ];
  ngOnInit() {
    this.brandService.getBrandById(this.activateRoute.snapshot.params['id'])
    .subscribe((res:any)=>{
      this.updateBrandForm = this.fb.group({
        brandname: new FormControl(res["brandName"]),
        imagepath: new FormControl(res["imagePath"]),
        userid: new FormControl(res["userId"])
      })
    });
  }

  // Handle the file input change event
  onFileChange(event: any) {
    //this.formData = [];
    // Get the selected file from the event
    const ImagePath = event.target.files[0] as File;
    // Update the form control value with the file name
    this.updateBrandForm.patchValue({
      ImagePath: ImagePath.name
    });
    // Create a new FormData object and append the file to it
    this.formData = new FormData();
    this.formData.append('ImagePath', ImagePath, ImagePath.name);
  }

  brandUpdated(id:number){
     // Get the other form values
     const name = this.updateBrandForm.get('brandname')?.value;
     const user = this.updateBrandForm.get('userid')?.value;
     // Append them to the FormData object
     this.formData.append('BrandName', name);
     this.formData.append('UserId', user);
     this.formData.append('CreatedAt', '');
     this.formData.append('UpdatedAt', '');
     // Make the HTTP post request to the Web API endpoint
     this.http.put("https://localhost:7201/api/Brand/updatebrand?id="+id, this.formData, 
     ).subscribe(response => {
        // Handle the response
        console.log(response);
        this.router.navigate(["get-brand"]);
      });
  }
}
