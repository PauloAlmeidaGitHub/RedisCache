using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;


namespace RedisCache.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    //[Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        public HealthCheckController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public string GetStatus()
        {
            // Objetivo: Duas operações (Get e Set)
            // Retornar o valor anterior do Cache e renova com o valor atual

            // Chave do cache
            var key = "HealthCheckStatus";

            // Read Cache antigo  (Deve ficar na API)
            var anterior = _distributedCache.GetString(key);   //(Lê objeto do cache)

            //Atualiza e prepara 
            //var actualStatus = $"{DateTime.UtcNow:o}";  // Letra o ==> formata
            var atual = $"{DateTime.Now}";


            // Write Cache atual (Em outra aplicação)
            _distributedCache.SetString(key, atual);   // Binary usa Set



            var healthCheckResult = $"Anterior: {anterior} => Atual {atual}";
            return healthCheckResult;
        }
    }
}



/*
 LINKS
//============================================
// https://www.youtube.com/watch?v=CwD3GFVR2dg
//============================================

REDIS - DOWNLOAD - Redis-x64-3.0.504.msi
https://github.com/microsoftarchive/redis/releases

STUDY
https://www.youtube.com/watch?v=cPXwdjx3R5Q
https://renatogroffe.medium.com/net-core-3-1-redis-do-cache-distribu%C3%ADdo-ao-uso-como-banco-nosql-a88d6da39e0
http://www.macoratti.net/20/10/aspc_inmcache1.htm

Memurai - segundo porte do servidor Redis para o sistema operacional Windows
http://www.macoratti.net/20/02/mem_primpa1.htm

http://www.macoratti.net/19/06/aspc_cache1.htm

http://www.macoratti.net/21/05/vda050521.htm
*/


/*
Chocolatey
REDIS => Chocoinstall redis ? (chocolatey)  Port 6379

Se a aplicação fizer o transacional Mediator para gravar no cache (REDIS) 

*/