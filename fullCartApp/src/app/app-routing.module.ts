import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';

import { GetCategoryComponent } from './Admin/Category/get-category/get-category.component';
import { AddCategoryComponent } from './Admin/Category/add-category/add-category.component';
import { EditCategoryComponent } from './Admin/Category/edit-category/edit-category.component';
import { GetBrandComponent } from './Admin/Brand/get-brand/get-brand.component';
import { EditBrandComponent } from './Admin/Brand/edit-brand/edit-brand.component';
import { AddBrandComponent } from './Admin/Brand/add-brand/add-brand.component';
import { GetProductComponent } from './Admin/Inventory/get-product/get-product.component';
import { AddProductComponent } from './Admin/Inventory/add-product/add-product.component';
import { EditProductComponent } from './Admin/Inventory/edit-product/edit-product.component';
import { ViewProductsComponent } from './Customer/view-products/view-products.component';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';
import { AuthGuard } from './authguard.guard';
import { HomeComponent } from './Components/home/home.component';

const routes: Routes = [
  { 
    path: '',
    pathMatch: 'full',
    redirectTo: 'login' 
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'get-category', component: GetCategoryComponent, },
  {
    path: 'add-category', component: AddCategoryComponent, 
  },
  {
    path: 'edit-category/:id', component: EditCategoryComponent,
  },
  {
    path: 'get-brand', component: GetBrandComponent, 
  },
  {
    path: 'add-brand', component: AddBrandComponent, 
  },
  {
    path: 'edit-brand/:id', component: EditBrandComponent, 
  },
  {
    path: 'get-product', component: GetProductComponent,
  },
  {
    path: 'add-product', component: AddProductComponent,
  },
  {
    path: 'edit-product/:id', component: EditProductComponent, 
  },
  {
    path: 'viewproduct', component: ViewProductsComponent,
  },
  {
    path:"home",
    component: HomeComponent
  },
  {
    path:"**",
    component: PageNotFoundComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
