import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/ItemModel';
import { ServicesService } from 'src/app/services.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  orderForm: FormGroup;
  itemForm: FormGroup;
  ListOfItems:any[];
  ListOfCustomer:any[]=[];
  itemslist:number[];
  itemslists:Item[]=[];
  items:Item;
  itemId:any;
  itemTotalPrice:any;
  nettotalPrice:number=0;
  itemPrices:any;

  constructor(private _fb:FormBuilder,
    public _personalService:ServicesService,
    private _toster:ToastrService
    ) { }

  ngOnInit(): void {
    this.itemslist=new Array<number>();
    this.ListOfItems=new Array<Item>();

    this.getAllItems();
    this.getAllCustomer();
    this.ceratFrom();
  }
  changevalue(item:any){
    this.itemPrices=item.itemPrice;
    this.itemForm.value.itemPrice=item.itemPrice;
    this.itemForm.value.itemId=item.itemId;
    this.itemForm.value.itemNames=item.itemName;
  }
  changeTotal(ttl:any){
    this.itemTotalPrice=ttl.target.value * this.itemPrices;
    this.itemForm.value.totalPrice=this.itemTotalPrice;
    this.itemForm.value.quentity=ttl.target.value;
  }
  
  deleteItem(id:number){
    this.ListOfItems.splice(id,1);
  }
  CreateItem(){
    this.itemForm=this._fb.group({
      itemId: [,[]],
      itemNames:[,[]],
      itemPrice:[,[]],
      quentity:[,[]],
      totalPrice:[,[]]
    })
  }
  addItem(){
    this.nettotalPrice=this.nettotalPrice + this.itemForm.value.totalPrice;
    this.itemslist.push(this.itemForm.value.itemId);
    this.ListOfItems.push(this.itemForm.value);
    this.CreateItem();
  }

  getAllItems(){
    this._personalService.getAllItems().subscribe(res=>{
      this.itemslists=res as any[];
    })
  }
  getAllCustomer(){
    this._personalService.getAllCustomer().subscribe(res=>{
      this.ListOfCustomer=res as any[];
    })
  }
  saveOrder(){
    this.orderForm.value.totalPrice=this.nettotalPrice;
    const formData:FormData = new FormData();
    formData.append('orderId',this.orderForm.value.orderId)
    formData.append('orderNumber',this.orderForm.value.orderNumber)
    formData.append('orderAdderss',this.orderForm.value.orderAdderss)
    formData.append('orderDate',this.orderForm.value.orderDate)
    formData.append('totalPrice',this.orderForm.value.totalPrice)
    formData.append('customerId',this.orderForm.value.customerId)
    for (let i = 0; i < this.orderForm.value.order_Item.length; i++) {
      formData.append("order_Items",this.orderForm.value.order_Item[i])
    }
    console.log("formData",formData)
    console.log("formData",this.orderForm.value)
    this._personalService.saveOrder(formData).subscribe((res:any)=>{
      if(res>0){
         this._toster.success('Order successfully saved');
      } 
      else{
        this._toster.error("Order not saved")

      }
    })
      this.ceratFrom();
      this.ListOfItems=new Array<number>();
  }

  ceratFrom(){
   this.orderForm=this._fb.group({
    orderId:[0,[]], 
    orderNumber:[,[]], 
    orderAdderss:[,[]], 
    orderDate:[,[]], 
    totalPrice:[,[]], 
    customerId:[,[]], 
    order_Item:[this.itemslist,[]],
    orderItems:this._fb.array([
      this.CreateItem()
    ]) 
   })
  }

  get f(){
    return this.orderForm.value;
  }
  get g(){
    return this.itemForm.value;
  }
}
