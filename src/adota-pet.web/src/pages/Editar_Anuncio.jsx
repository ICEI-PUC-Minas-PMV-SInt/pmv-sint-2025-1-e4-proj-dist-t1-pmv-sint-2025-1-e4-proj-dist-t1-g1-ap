import { useEffect, useState } from 'react'
import { useParams, useNavigate } from 'react-router-dom' // Importar useNavigate
import { Navbar } from '../components/Navbar'
import { api } from '../helpers/api'

function Editar_Anuncio() {
  const { id } = useParams()
  const navigate = useNavigate() // Hook para navegação
  const [formData, setFormData] = useState({
    titulo: '',
    descricao: '',
    categoria: '',
    raca: '',
    idade: '',
  })
  const [warning, setWarning] = useState(null)
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    async function fetchData() {
      try {
        const response = await api.get(`/Anuncios/${id}`)
        setFormData({
          titulo: response.data.titulo,
          descricao: response.data.descricao,
          categoria: response.data.categoria,
          raca: response.data.raca,
          idade: response.data.idade,
        })
      } catch (err) {
        console.error(err)
        setWarning('Erro ao carregar os dados do anúncio.')
      }
    }
    fetchData()
  }, [id])

  const handleChange = e => {
    const { id, value } = e.target
    setFormData(prev => ({
      ...prev,
      [id]: value,
    }))
  }

  const handleSubmit = async event => {
    event.preventDefault()
    setLoading(true)
    setWarning(null)

    await api
      .put(`/Anuncios/${id}`, {
        ...formData,
        ativo: true,
      })
      .then(res => {
        setLoading(false)
        setWarning(`Anúncio atualizado com sucesso!`)
      })
      .catch(err => {
        setLoading(false)
        console.error(err)
        setWarning(
          `Erro "${err.message}", consulte o console para mais informações`,
        )
      })
  }

  return (
    <div className='font-inter h-screen w-screen bg-[#efefef] antialiased'>
      <Navbar />
      <div className='flex h-full w-full items-center justify-center'>
        <div className='relative w-full max-w-xs'>
          <div className='w-full overflow-hidden rounded-xl shadow-2xl shadow-neutral-300'>
            <h1 className='w-full bg-[#3b5253] py-5 text-center tracking-wider text-white'>
              Editar Anúncio
            </h1>
            <form
              className='flex w-full flex-col items-center gap-3 px-2.5 py-2.5'
              onSubmit={handleSubmit}
            >
              <Input
                id='titulo'
                value={formData.titulo}
                onChange={handleChange}
                placeholder='Título do anúncio'
              />
              <Input
                id='descricao'
                value={formData.descricao}
                onChange={handleChange}
                placeholder='Descrição'
              />
              <SelectCategoria
                id='categoria'
                value={formData.categoria}
                onChange={handleChange}
              />
              <Input
                id='raca'
                value={formData.raca}
                onChange={handleChange}
                placeholder='Raça'
              />
              <Input
                id='idade'
                value={formData.idade}
                onChange={handleChange}
                placeholder='Idade'
              />

              <div className='flex w-full gap-2'>
                <button
                  type='button'
                  onClick={() => navigate('/anuncios')} // Redireciona para /anuncios
                  className='flex-1 cursor-pointer rounded-sm bg-[#cccccc] px-6 py-2 text-[#333] transition-all hover:scale-105 active:scale-95'
                >
                  Voltar
                </button>
                <input
                  type='submit'
                  className='flex-1 cursor-pointer rounded-sm bg-[#5e8a8c] px-6 py-2 text-white transition-all hover:scale-105 active:scale-95'
                  value='Salvar'
                />
              </div>
            </form>
          </div>
          <p className='absolute -bottom-36 h-32 w-full text-center text-lg text-[#656565]'>
            {loading && 'Salvando...'}
            {warning && warning}
          </p>
        </div>
      </div>
    </div>
  )
}

function Input({ id, value, onChange, placeholder }) {
  return (
    <input
      id={id}
      value={value}
      onChange={onChange}
      placeholder={placeholder}
      className='w-full rounded-lg bg-[#ffffff] px-2 py-2.5 text-[#4f4f4f] outline-0 transition-all placeholder:font-bold placeholder:text-[#b1b1b1] placeholder:italic hover:scale-105 focus:scale-105'
      required
    />
  )
}

function SelectCategoria({ id, value, onChange }) {
  return (
    <select
      id={id}
      value={value}
      onChange={onChange}
      required
      className='w-full rounded-lg bg-[#ffffff] px-2 py-2.5 font-bold text-[#4f4f4f] outline-0 transition-all hover:scale-105 focus:scale-105'
    >
      <option value='' disabled>
        Selecione a categoria
      </option>
      <option value='Canino'>Canino</option>
      <option value='Felino'>Felino</option>
      <option value='Ave'>Ave</option>
    </select>
  )
}

export { Editar_Anuncio }
