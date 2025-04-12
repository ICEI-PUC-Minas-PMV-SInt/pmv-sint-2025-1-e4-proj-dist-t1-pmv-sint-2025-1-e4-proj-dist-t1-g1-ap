import { useEffect, useState } from 'react'
import { Navbar } from '../components/Navbar'
import { api } from '../helpers/api'
import { useNavigate } from 'react-router-dom' // 👈 IMPORTANTE

function Anuncios() {
  const [anuncios, setAnuncios] = useState(null)
  const [warning, setWarning] = useState('')

  useEffect(() => {
    api
      .get('/Anuncios')
      .then(res => setAnuncios(res.data))
      .catch(err => {
        console.log(err)
        setWarning(
          `Erro "${err.message}", consulte o console para mais informações`,
        )
      })
  }, [])

  return (
    <div className='font-inter min-h-screen w-screen bg-[#efefef] antialiased'>
      <Navbar />
      <div className='p-6'>
        <h1 className='mb-8 text-center text-2xl font-semibold text-[#3b5253]'>
          Anúncios
        </h1>
        <div className='flex flex-col gap-6'>
          {warning && <p>{warning}</p>}
          {anuncios &&
            anuncios.map(anuncio => (
              <AnuncioCard key={anuncio.id} anuncio={anuncio} />
            ))}
        </div>
      </div>
    </div>
  )
}

function AnuncioCard({ anuncio }) {
  const [liked, setLiked] = useState(false)
  const navigate = useNavigate() // 👈 Hook de navegação

  return (
    <div className='rounded-xl bg-white p-6 shadow-md transition-all hover:scale-[1.01]'>
      <div className='grid grid-cols-2 gap-4 text-sm font-medium text-[#333] sm:grid-cols-3 lg:grid-cols-6'>
        <div>
          <span className='block text-xs text-gray-500'>Título</span>
          {anuncio.titulo}
        </div>
        <div>
          <span className='block text-xs text-gray-500'>Descrição</span>
          {anuncio.descricao}
        </div>
        {/*<div>
          <span className='block text-xs text-gray-500'>Categoria</span>
          {anuncio.categoria}
        </div>*/}
        <div>
          <span className='block text-xs text-gray-500'>Raça</span>
          {anuncio.racaAnimal}
        </div>
        <div>
          <span className='block text-xs text-gray-500'>Idade</span>
          {Number(anuncio.idadeAnimal)}{' '}
          {Number(anuncio.idadeAnimal) <= 1 ? 'ano' : 'anos'}
        </div>
        <div className='mt-4 flex flex-wrap items-center gap-2 lg:mt-0'>
          <button
            onClick={() => alert(`Detalhes do anúncio ID: ${anuncio.id}`)}
            className='cursor-pointer rounded-md bg-[#5e8a8c] px-4 py-2 text-white transition-all hover:scale-105 active:scale-95'
          >
            Detalhes
          </button>
          <button
            onClick={() => navigate(`/editar_anuncio/${anuncio.id}`)} // 👈 Redireciona
            className='cursor-pointer rounded-md bg-yellow-500 px-4 py-2 text-white transition-all hover:scale-105 active:scale-95'
          >
            Editar
          </button>
          <button
            onClick={() => setLiked(!liked)}
            className='cursor-pointer rounded-md bg-pink-400 px-4 py-2 text-white transition-all hover:scale-105 active:scale-95'
          >
            {liked ? 'Descurtir' : 'Curtir'}
          </button>
        </div>
      </div>
    </div>
  )
}

export { Anuncios }
