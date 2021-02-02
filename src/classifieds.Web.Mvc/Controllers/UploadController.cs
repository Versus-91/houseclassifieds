using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Validation;
using classifieds.Authorization.Users;
using classifieds.Filters;
using classifieds.Images;
using classifieds.Web.helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Controllers
{
    public class UploadController : classifiedsControllerBase
    {
        private readonly long _fileSizeLimit;
        private readonly ILogger<UploadController> _logger;
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg" };
        private readonly string _postImagesFilePath;
        private readonly string _avatarsFilePath;
        private readonly UserManager _userManager;
        private readonly IRepository<Image> _imageService;
        private readonly IWebHostEnvironment _env;

        // Get the default form options so that we can use them to set the default 
        // limits for request body data.
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public UploadController(ILogger<UploadController> logger,
            IRepository<Image> imageService,
            IConfiguration config,
            UserManager userManager,
            IWebHostEnvironment env
            )
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            _env = env;
            // To save physical files to a path provided by configuration:
            _postImagesFilePath = config.GetValue<string>("ImagesFilesPath");
            _avatarsFilePath = config.GetValue<string>("AvatarsFilesPath");
            _logger = logger;
            // To save physical files to the temporary files folder, use:
            //_targetFilePath = Path.GetTempPath();
            _imageService = imageService;
            _userManager = userManager;
        }

        [HttpPost("[controller]/remove/{id}")]
    
        [AbpMvcAuthorize]
        public async Task<IActionResult> removeImage(int id)
        {
            var image =await  _imageService.GetAllIncluding(m=>m.Post).Where(m=>m.Id == id).FirstOrDefaultAsync();
            if (image == null)
            {
                return NotFound();
            }
            if (AbpSession.UserId != image.Post.CreatorUserId)
            {
                return BadRequest();
            }
            await _imageService.DeleteAsync(image);
            System.IO.File.Delete(Path.Combine(_env.WebRootPath,image.Path));
            return Ok();
        }
        #region snippet_UploadPhysical
        [HttpPost("[controller]/{id}")]
        [DisableFormValueModelBinding]
        [DisableValidation]
        //[ValidateAntiForgeryToken]
        [AbpMvcAuthorize]
        public async Task<IActionResult> UploadPhysical(int id)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                ModelState.AddModelError("File",
                    $"The request couldn't be processed (Error 1).");
                // Log error

                return BadRequest(ModelState);
            }

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // This check assumes that there's a file
                    // present without form data. If form data
                    // is present, this method immediately fails
                    // and returns the model error.
                    if (!MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition))
                    {
                        ModelState.AddModelError("File",
                            $"The request couldn't be processed (Error 2).");
                        // Log error

                        return BadRequest(ModelState);
                    }
                    else
                    {
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                contentDisposition.FileName.Value);
                        var trustedFileNameForFileStorage = Path.Combine($"{Guid.NewGuid().ToString("N")}{Path.GetExtension(trustedFileNameForDisplay).ToLower()}"); ;

                        // **WARNING!**
                        // In the following example, the file is saved without
                        // scanning the file's contents. In most production
                        // scenarios, an anti-virus/anti-malware scanner API
                        // is used on the file before making the file available
                        // for download or for use by other systems. 
                        // For more information, see the topic that accompanies 
                        // this sample.

                        var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                            section, contentDisposition, ModelState,
                            _permittedExtensions, _fileSizeLimit);

                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }
         

                        Directory.CreateDirectory(Path.Combine(_env.WebRootPath, _postImagesFilePath));
                        
                            using (var targetStream = System.IO.File.Create(Path.Combine(_env.WebRootPath, _postImagesFilePath, trustedFileNameForFileStorage)))
                            {
                                await targetStream.WriteAsync(streamedFileContent);
                                await _imageService.InsertAsync(new Image
                                {
                                    Name = trustedFileNameForDisplay,
                                    Size = contentDisposition.Size ?? 0,
                                    Path = Path.Combine(_postImagesFilePath, trustedFileNameForFileStorage),
                                    PostId = id
                                });
                                _logger.LogInformation(
                                    "Uploaded file '{TrustedFileNameForDisplay}' saved to " +
                                    "'{TargetFilePath}' as {TrustedFileNameForFileStorage}",
                                    trustedFileNameForDisplay, _postImagesFilePath,
                                    trustedFileNameForFileStorage);
                            }
                        
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            return Ok("images saved successfully.");
        }
        #endregion
        [HttpPost("[controller]/avatar")]
        [DisableFormValueModelBinding]
        [DisableValidation]
        [AbpMvcAuthorize]
        //[ValidateAntiForgeryToken]

        public async Task<IActionResult> UploadAvatar()
        {

            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                ModelState.AddModelError("File",
                    $"The request couldn't be processed (Error 1).");
                // Log error

                return BadRequest(ModelState);
            }

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // This check assumes that there's a file
                    // present without form data. If form data
                    // is present, this method immediately fails
                    // and returns the model error.
                    if (!MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition))
                    {
                        ModelState.AddModelError("File",
                            $"The request couldn't be processed (Error 2).");
                        // Log error

                        return BadRequest(ModelState);
                    }
                    else
                    {
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                contentDisposition.FileName.Value);
                        var trustedFileNameForFileStorage = Path.Combine($"{Guid.NewGuid().ToString("N")}{Path.GetExtension(trustedFileNameForDisplay).ToLower()}");

                        // **WARNING!**
                        // In the following example, the file is saved without
                        // scanning the file's contents. In most production
                        // scenarios, an anti-virus/anti-malware scanner API
                        // is used on the file before making the file available
                        // for download or for use by other systems. 
                        // For more information, see the topic that accompanies 
                        // this sample.

                        var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                            section, contentDisposition, ModelState,
                            _permittedExtensions, _fileSizeLimit);

                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }
                        Directory.CreateDirectory(Path.Combine(_env.WebRootPath, _avatarsFilePath));
                        using (var targetStream = System.IO.File.Create(Path.Combine(_env.WebRootPath, _avatarsFilePath, trustedFileNameForFileStorage)))
                        {
                            await targetStream.WriteAsync(streamedFileContent);
                            var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
                            if (!String.IsNullOrEmpty(user.Avatar))
                            {
                                System.IO.File.Delete(Path.Combine(_env.WebRootPath, user.Avatar));
                            }
                            user.Avatar = Path.Combine(_avatarsFilePath, trustedFileNameForFileStorage);
                            await _userManager.UpdateAsync(user);
                        }
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            return Ok("images saved successfully.");
        }
        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader =
                MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

            // UTF-7 is insecure and shouldn't be honored. UTF-8 succeeds in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }

            return mediaType.Encoding;
        }
    }
}
