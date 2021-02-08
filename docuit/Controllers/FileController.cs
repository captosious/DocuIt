using System;
using System.Collections.Generic;
using System.Linq;
using DocuItService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocuItService.Controllers
{
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }

        // download file(s) to client according path: rootDirectory/subDirectory with single zip file
        [HttpGet("Download/{subDirectory}")]
        public IActionResult DownloadFiles(string subDirectory)
        {
            try
            {
                var (fileType, archiveData, archiveName) = _fileService.FetechFiles(subDirectory);

                return File(archiveData, fileType, archiveName);
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        // upload file(s) to server that palce under path: rootDirectory/subDirectory
        [HttpPost("upload")]
        public IActionResult UploadFile([FromForm(Name = "")] List<IFormFile> files, string subDirectory)
        {
            try
            {
                _fileService.SaveFile(files, subDirectory);

                return Ok(new { files.Count, Size = FileService.SizeConverter(files.Sum(f => f.Length)) });
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }
    }
}
