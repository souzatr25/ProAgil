import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventosFiltrados: Evento[];
  eventos: Evento[];
  imagemLargura: number = 50;
  imagemMargem: number = 2;
  mostrarImagem : boolean = false;
  modalRef: BsModalRef;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService
    ) { }

  _filtroLista: string = "";
  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(value:string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventosFiltrados;
  } 

  openModal(template: TemplateRef<any>){
    this.modalRef = this.modalService.show(template);
  }

  

  ngOnInit() {
    this.getEventos();
  }

  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  filtrarEventos(filtrarPor: string): Evento[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  getEventos(){
    this.eventoService.getAllEvento().subscribe(
      (_eventos: Evento[])=> {
      this.eventos = _eventos;
      console.log(_eventos);
    }, error => {
      console.log(error);
    });
  }

}
