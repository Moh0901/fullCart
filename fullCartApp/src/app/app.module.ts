import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/header/header.component';
import { LoginComponent } from './Components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginServiceService } from './Services/login-service.service';
import { TokenHandlerInterceptor } from './token-handler.interceptor';
import { GetCategoryComponent } from './Admin/Category/get-category/get-category.component';
import { AddCategoryComponent } from './Admin/Category/add-category/add-category.component';
import { CategoryService } from './Services/category.service';
import { EditCategoryComponent } from './Admin/Category/edit-category/edit-category.component';
import { GetBrandComponent } from './Admin/Brand/get-brand/get-brand.component';
import { AddBrandComponent } from './Admin/Brand/add-brand/add-brand.component';
import { EditBrandComponent } from './Admin/Brand/edit-brand/edit-brand.component';
import { AddProductComponent } from './Admin/Inventory/add-product/add-product.component';
import { EditProductComponent } from './Admin/Inventory/edit-product/edit-product.component';
import { GetProductComponent } from './Admin/Inventory/get-product/get-product.component';
import { ViewProductsComponent } from './Customer/view-products/view-products.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginComponent,
    GetCategoryComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    GetBrandComponent,
    AddBrandComponent,
    EditBrandComponent,
    AddProductComponent,
    EditProductComponent,
    GetProductComponent,
    ViewProductsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [LoginServiceService, CategoryService,
    {
      provide:HTTP_INTERCEPTORS, useClass:TokenHandlerInterceptor, multi:true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
