namespace MyBlog.Service
{
    public class Mapper
    {
        /// <summary>
        /// Verilen source tipinden target tipine mapping yapar.
        /// </summary>
        /// <typeparam name="S">Source tipi</typeparam>
        /// <typeparam name="T">Target tipi</typeparam>
        /// <param name="source">Source nesne</param>
        /// <returns>Maplenen target nesnesi</returns>
        public static T Map<S, T>(object source)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<S, T>();
            });

            AutoMapper.IMapper mapper = config.CreateMapper();
            var result = mapper.Map<S, T>((S)source);

            return result;
        }
    }
}
