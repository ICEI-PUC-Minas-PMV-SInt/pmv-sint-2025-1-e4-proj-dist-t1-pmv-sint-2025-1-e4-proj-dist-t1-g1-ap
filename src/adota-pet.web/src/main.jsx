import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './main.css'
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom'

import { Cadastro } from './pages/Cadastro'
import { Usuario } from './pages/Usuario'
import { Anuncios } from './pages/Anuncios'
import { CriarAnuncio } from './pages/CriarAnuncio'
import { EditarAnuncio } from './pages/EditarAnuncio'
import { Login } from './pages/Login'
import { AnuncioDetalhes } from './pages/AnuncioDetalhes'
import { Favoritos } from './pages/Favoritos'
import { Denunciados } from './pages/Denunciados'

import { Navbar } from './components/Navbar'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <Navbar />
      <Routes>
        {/* Redireciona '/' para uma página válida */}
        <Route path='/' element={<Navigate to='/anuncios' replace />} />
        {/* Rotas reais */}
        <Route path='/cadastro' element={<Cadastro />} />
        <Route path='/usuario' element={<Usuario />} />
        <Route path='/anuncios' element={<Anuncios />} />
        <Route path='/criar_anuncio' element={<CriarAnuncio />} />
        <Route path='/editar_anuncio/:id' element={<EditarAnuncio />} />
        <Route path='/login' element={<Login />} />
        <Route path='/anuncio/:id' element={<AnuncioDetalhes />} />
        <Route path='/favoritos' element={<Favoritos />} />
        <Route path='/denunciados' element={<Denunciados />} />
        {/* ✅ adicione aqui */}
      </Routes>
    </BrowserRouter>
  </StrictMode>,
)
