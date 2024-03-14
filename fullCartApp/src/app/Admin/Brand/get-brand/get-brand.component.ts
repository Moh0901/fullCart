import { Component } from '@angular/core';
import { BrandService } from 'src/app/Services/brand.service';

@Component({
  selector: 'app-get-brand',
  templateUrl: './get-brand.component.html',
  styleUrls: ['./get-brand.component.css']
})
export class GetBrandComponent {

  brand: any = [];
  constructor(private brandService: BrandService) { }

  ngOnInit(): void {
    this.getbrand();
  }

  getbrand(){
      this.brandService.getAllBrand().subscribe((data)=>{
        console.log(data);
        this.brand=data;
      })
    }

    deletebrand(id: number){
      this.brandService.deleteBrand(id).subscribe(() =>{
        this.getbrand();
      });
      alert("Delete Successfully");
    }
}
