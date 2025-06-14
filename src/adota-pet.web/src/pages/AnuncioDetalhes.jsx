import { useEffect, useState } from 'react'
import { useParams, useNavigate, useLocation } from 'react-router-dom'
import { api } from '../helpers/api'
import { Navbar } from '../components/Navbar'

function AnuncioDetalhes() {
  const { id } = useParams()
  const navigate = useNavigate()
  const location = useLocation()
  const fromDenunciados = location.state?.from === 'denunciados'

  const [anuncio, setAnuncio] = useState(null)
  const [warning, setWarning] = useState('')
  const [jaDenunciado, setJaDenunciado] = useState(false)

  const isLoggedIn = !!localStorage.getItem('token')

  useEffect(() => {
    if (!isLoggedIn) {
      navigate('/login')
      return
    }

    api
      .get(`/Anuncios/${id}`)
      .then(res => {
        setAnuncio(res.data)

        // Verifica se o anúncio já está na lista de denunciados
        const denunciados =
          JSON.parse(localStorage.getItem('denunciados')) || []
        const jaExiste = denunciados.some(item => item.id === res.data.id)
        setJaDenunciado(jaExiste)
      })
      .catch(err => {
        console.error(err)
        setWarning('Erro ao carregar anúncio.')
      })
  }, [id, isLoggedIn, navigate])

  const handleDenunciaToggle = () => {
    const denunciados = JSON.parse(localStorage.getItem('denunciados')) || []

    if (jaDenunciado) {
      const atualizados = denunciados.filter(item => item.id !== anuncio.id)
      localStorage.setItem('denunciados', JSON.stringify(atualizados))
      setJaDenunciado(false)
    } else {
      localStorage.setItem(
        'denunciados',
        JSON.stringify([...denunciados, anuncio]),
      )
      setJaDenunciado(true)
    }
  }

  if (!isLoggedIn) return null

  return (
    <div className='font-inter min-h-screen bg-gray-100'>
      <Navbar />
      <div className='flex min-h-[calc(100vh-64px)] items-center justify-center px-4 py-12'>
        <div className='w-full max-w-2xl rounded-xl bg-white p-8 shadow-md'>
          {warning && <p className='text-red-600'>{warning}</p>}
          {anuncio ? (
            <>
              <h1 className='mb-4 text-center text-3xl font-bold'>
                {anuncio.titulo}
              </h1>
              <div className='space-y-2 text-gray-800'>
                <p>
                  <strong>Descrição:</strong> {anuncio.descricao}
                </p>
                <p>
                  <strong>Raça:</strong> {anuncio.racaAnimal}
                </p>
                <p>
                  <strong>Idade:</strong> {anuncio.idadeAnimal}{' '}
                  {anuncio.idadeAnimal <= 1 ? 'ano' : 'anos'}
                </p>
              </div>
              <div className='mt-6 flex flex-wrap justify-center gap-2'>
                <button
                  onClick={() =>
                    navigate(fromDenunciados ? '/denunciados' : '/anuncios')
                  }
                  className='min-w-[120px] cursor-pointer rounded-md bg-[#5e8a8c] px-4 py-2 text-center text-white transition-all hover:scale-105 active:scale-95'
                >
                  Voltar
                </button>

                {jaDenunciado && (
                  <button
                    onClick={handleDenunciaToggle}
                    className={`min-w-[120px] cursor-pointer rounded-md bg-red-500 px-4 py-2 text-center text-white transition-all hover:scale-105 active:scale-95`}
                  >
                    {jaDenunciado ? 'Remover denúncia' : 'Denunciar'}
                  </button>
                )}
              </div>
            </>
          ) : (
            <p className='text-center'>Carregando...</p>
          )}
        </div>
      </div>
    </div>
  )
}

export { AnuncioDetalhes }
