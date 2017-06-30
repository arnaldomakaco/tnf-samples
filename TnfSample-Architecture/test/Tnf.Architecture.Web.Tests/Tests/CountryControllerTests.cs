﻿using Xunit;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Tnf.Application.Services.Dto;
using Tnf.Architecture.Dto;
using System.Net;
using Tnf.Architecture.Web.Controllers;
using Tnf.AspNetCore.Mvc.Response;
using System.Linq;
using Tnf.App.Dto.Response;
using Tnf.App.Crud;

namespace Tnf.Architecture.Web.Tests.Tests
{
    public class CountryControllerTests : AppTestBase
    {
        [Fact]
        public void Should_Resolve_Controller()
        {
            ServiceProvider.GetService<CountryController>().ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAll_Countries_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CountryDto>>(
                $"/{RouteConsts.Country}",
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.Items.Count, 5);
        }

        [Fact]
        public async Task GetAll_Countries_With_Paginated()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CountryDto>>(
                $"/{RouteConsts.Country}?pageSize=3",
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.Total, 5);
            Assert.Equal(response.HasNext, true);
            Assert.Equal(response.Items.Count, 3);
        }

        [Fact]
        public async Task GetAll_Countries_Filtering_By_Name_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CountryDto>>(
                               $"{RouteConsts.Country}?name=EUA",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.Items.Count, 1);
            Assert.Equal(response.Items[0].Name, "EUA");
        }

        [Fact]
        public async Task GetAll_Countries_Sorted_ASC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CountryDto>>(
                               $"{RouteConsts.Country}?order=name",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.Items.Count, 5);
            Assert.Equal(response.Items[0].Name, "Brasil");
        }

        [Fact]
        public async Task GetAll_Countries_Sorted_DESC_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CountryDto>>(
                               $"{RouteConsts.Country}?order=-name",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.Items.Count, 5);
            Assert.Equal(response.Items[0].Name, "Venezuela");
        }


        [Fact]
        public async Task Get_Country_With_Success()
        {
            // Act
            var response = await GetResponseAsObjectAsync<CountryDto>(
                $"/{RouteConsts.Country}/1",
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.Id, 1);
            Assert.Equal(response.Name, "Brasil");
        }

        [Fact]
        public async Task Get_Country_Fields_With_Sucess()
        {
            // Act
            var response = await GetResponseAsObjectAsync<CountryDto>(
                               $"/{RouteConsts.Country}/1?fields=name",
                               HttpStatusCode.OK
                           );

            // Assert
            Assert.Equal(response.Id, 0);
            Assert.Equal(response.Name, "Brasil");
        }

        [Fact]
        public async Task Get_Country_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                $"/{RouteConsts.Country}/0",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("GetCountry");
            response.DetailedMessage.ShouldBe("GetCountry");
            Assert.True(response.Details.Any(a => a.Message == CrudOperations.TnfAppCrud_InvalidParameterError.ToString()));
        }

        [Fact]
        public async Task Get_Country_When_Not_Exits_Return_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                $"/{RouteConsts.Country}/99",
                HttpStatusCode.NotFound
            );

            // Assert
            response.Message.ShouldBe("GetCountry");
            response.DetailedMessage.ShouldBe("GetCountry");
            Assert.True(response.Details.Any(a => a.Message == CrudOperations.TnfAppCrud_OnGetCouldNotFind.ToString()));
        }


        [Fact]
        public async Task Post_Country_With_Success()
        {
            //Arrange
            var countryDto = new CountryDto()
            {
                Id = 6,
                Name = "Canada"
            };

            // Act
            var response = await PostResponseAsObjectAsync<CountryDto, CountryDto>(
                $"/{RouteConsts.Country}",
                countryDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.Id, 6);
            Assert.Equal(response.Name, "Canada");
        }

        [Fact]
        public async Task Post_Professional_Should_Be_Insert_And_Update_Item()
        {
            //Arrange
            var countryDto = new CountryDto()
            {
                Id = 6,
                Name = "Canada"
            };

            // Act
            var response = await PostResponseAsObjectAsync<CountryDto, CountryDto>(
                $"/{RouteConsts.Country}",
                countryDto,
                HttpStatusCode.OK
            );

            // Assert
            response.Id.ShouldBe(6);

            var updateParam = new CountryDto()
            {
                Name = "País Alterado Teste"
            };

            // Act
            await PutResponseAsObjectAsync<CountryDto, CountryDto>(
                $"/{RouteConsts.Country}/{response.Id}",
                updateParam,
                HttpStatusCode.OK
            );

            response = await GetResponseAsObjectAsync<CountryDto>(
                $"/{RouteConsts.Country}/{response.Id}",
                HttpStatusCode.OK
            );

            //Assert
            response.Name.ShouldBe("País Alterado Teste");
        }

        [Fact]
        public async Task Post_Null_Country_Return_Bad_Request()
        {
            // Act
            var response = await PostResponseAsObjectAsync<CountryDto, ErrorResponse>(
                $"/{RouteConsts.Country}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PostCountry");
            response.DetailedMessage.ShouldBe("PostCountry");
            Assert.True(response.Details.Any(a => a.Message == CrudOperations.TnfAppCrud_InvalidParameterError.ToString()));
        }


        [Fact]
        public async Task Put_Country_With_Success()
        {
            // Arrange
            var countryDto = new CountryDto()
            {
                Id = 4,
                Name = "Canada"
            };

            // Act
            var response = await PutResponseAsObjectAsync<CountryDto, CountryDto>(
                $"/{RouteConsts.Country}/4",
                countryDto,
                HttpStatusCode.OK
            );

            // Assert
            Assert.Equal(response.Id, 4);
            Assert.Equal(response.Name, "Canada");
        }

        [Fact]
        public async Task Put_Country_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CountryDto, ErrorResponse>(
                $"{RouteConsts.Country}/0",
                new CountryDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutCountry");
            response.DetailedMessage.ShouldBe("PutCountry");
            Assert.True(response.Details.Any(a => a.Message == CrudOperations.TnfAppCrud_InvalidParameterError.ToString()));
        }

        [Fact]
        public async Task Put_Null_Country_Return_Bad_Request()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CountryDto, ErrorResponse>(
                $"/{RouteConsts.Country}/%20",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("PutCountry");
            response.DetailedMessage.ShouldBe("PutCountry");
            response.Details.Count.ShouldBe(2);
            Assert.True(response.Details.Any(a => a.Message == CrudOperations.TnfAppCrud_InvalidParameterError.ToString()));
        }

        [Fact]
        public async Task Put_Country_When_Not_Exits_Return_Not_Found_Request()
        {
            // Arrange
            var countryDto = new CountryDto()
            {
                Id = 99,
                Name = "Canada"
            };

            // Act
            var response = await PutResponseAsObjectAsync<CountryDto, ErrorResponse>(
                $"/{RouteConsts.Country}/99",
                countryDto,
                HttpStatusCode.NotFound
            );

            // Assert
            response.Message.ShouldBe("PutCountry");
            response.DetailedMessage.ShouldBe("PutCountry");
            Assert.True(response.Details.Any(a => a.Message == CrudOperations.TnfAppCrud_OnGetCouldNotFind.ToString()));
        }


        [Fact]
        public async Task Delete_Country_With_Success()
        {
            // Act
            await DeleteResponseAsObjectAsync<string>(
                $"/{RouteConsts.Country}/1",
                HttpStatusCode.OK
            );
        }

        [Fact]
        public async Task Delete_Country_With_Invalid_Parameter_Return_Bad_Request()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponse>(
                $"/{RouteConsts.Country}/0",
                HttpStatusCode.BadRequest
            );

            // Assert
            response.Message.ShouldBe("DeleteCountry");
            response.DetailedMessage.ShouldBe("DeleteCountry");
            Assert.True(response.Details.Any(a => a.Message == CrudOperations.TnfAppCrud_InvalidParameterError.ToString()));
        }
    }
}
