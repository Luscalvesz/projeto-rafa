using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models {
    public partial class Time2EOLContext : DbContext {
        public Time2EOLContext() {
        }

        public Time2EOLContext(DbContextOptions<Time2EOLContext> options)
            : base(options) {
        }

        public virtual DbSet<Anuncio> Anuncio { get; set; }
        public virtual DbSet<Conservacao> Conservacao { get; set; }
        public virtual DbSet<Fabricante> Fabricante { get; set; }
        public virtual DbSet<Ficha> Ficha { get; set; }
        public virtual DbSet<Imagem> Imagem { get; set; }
        public virtual DbSet<Interesse> Interesse { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Time2EOL;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Anuncio>(entity => {
                entity.HasKey(e => e.IdAnuncio)
                    .HasName("PK__Anuncio__0BC1EC3E2F95BC3C");

                entity.Property(e => e.StatusAnuncio)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Inativo')");

                entity.HasOne(d => d.FkIdConservacaoNavigation)
                    .WithMany(p => p.Anuncio)
                    .HasForeignKey(d => d.FkIdConservacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Anuncio__FK_idCo__4AB81AF0");

                entity.HasOne(d => d.FkIdImagemNavigation)
                    .WithMany(p => p.AnuncioFkIdImagemNavigation)
                    .HasForeignKey(d => d.FkIdImagem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Anuncio__fk_idIm__4CA06362");

                entity.HasOne(d => d.FkIdImagem02Navigation)
                    .WithMany(p => p.AnuncioFkIdImagem02Navigation)
                    .HasForeignKey(d => d.FkIdImagem02)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Anuncio__fk_idIm__4D94879B");

                entity.HasOne(d => d.FkIdImagem03Navigation)
                    .WithMany(p => p.AnuncioFkIdImagem03Navigation)
                    .HasForeignKey(d => d.FkIdImagem03)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Anuncio__fk_idIm__4E88ABD4");

                entity.HasOne(d => d.FkIdProdutoNavigation)
                    .WithMany(p => p.Anuncio)
                    .HasForeignKey(d => d.FkIdProduto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Anuncio__FK_idPr__4BAC3F29");
            });

            modelBuilder.Entity<Conservacao>(entity => {
                entity.HasKey(e => e.IdConservacao)
                    .HasName("PK__Conserva__B3B6B0D3A6B44930");

                entity.Property(e => e.EstadoConservacao).IsUnicode(false);
            });

            modelBuilder.Entity<Fabricante>(entity => {
                entity.HasKey(e => e.IdFabricante)
                    .HasName("PK__Fabrican__6E91D599AD2E14DF");

                entity.Property(e => e.NomeFabricante).IsUnicode(false);
            });

            modelBuilder.Entity<Ficha>(entity => {
                entity.HasKey(e => e.IdFicha)
                    .HasName("PK__Ficha__846E2F761A03103D");

                entity.Property(e => e.ArmazenamentoFicha).IsUnicode(false);

                entity.Property(e => e.AudioFicha).IsUnicode(false);

                entity.Property(e => e.MemoriaFicha).IsUnicode(false);

                entity.Property(e => e.PlacaVideoFicha).IsUnicode(false);

                entity.Property(e => e.ProcessadorFicha).IsUnicode(false);

                entity.Property(e => e.SistOpFicha).IsUnicode(false);

                entity.Property(e => e.TelaFicha).IsUnicode(false);
            });

            modelBuilder.Entity<Imagem>(entity => {
                entity.HasKey(e => e.IdImagem)
                    .HasName("PK__Imagem__EA9A71373DA2F4F8");

                entity.Property(e => e.AltImagem).IsUnicode(false);

                entity.Property(e => e.Imagem1).IsUnicode(false);
            });

            modelBuilder.Entity<Interesse>(entity => {
                entity.HasKey(e => e.IdInteresse)
                    .HasName("PK__Interess__EC19CAE54B5177AE");

                entity.HasOne(d => d.FkIdAnuncioNavigation)
                    .WithMany(p => p.Interesse)
                    .HasForeignKey(d => d.FkIdAnuncio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Interesse__FK_id__52593CB8");

                entity.HasOne(d => d.FkIdUsuarioNavigation)
                    .WithMany(p => p.Interesse)
                    .HasForeignKey(d => d.FkIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Interesse__FK_id__5165187F");
            });

            modelBuilder.Entity<Produto>(entity => {
                entity.HasKey(e => e.IdProduto)
                    .HasName("PK__Produto__5EEDF7C338C8B1B9");

                entity.Property(e => e.ModeloProduto).IsUnicode(false);

                entity.Property(e => e.NomeProduto).IsUnicode(false);

                entity.HasOne(d => d.FkIdFabricanteNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.FkIdFabricante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Produto__FK_idFa__412EB0B6");

                entity.HasOne(d => d.FkIdFichaNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.FkIdFicha)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Produto__FK_idFi__4222D4EF");

                entity.HasOne(d => d.FkIdUsuarioNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.FkIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Produto__FK_idUs__4316F928");
            });

            modelBuilder.Entity<TipoUsuario>(entity => {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK__TipoUsua__03006BFF60188280");

                entity.Property(e => e.PermissaoTipoUsuario)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Funcionario')");
            });

            modelBuilder.Entity<Usuario>(entity => {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__645723A6F695B73E");

                entity.Property(e => e.EmailUsuario).IsUnicode(false);

                entity.Property(e => e.NomeUsuario).IsUnicode(false);

                entity.Property(e => e.SenhaUsuario).IsUnicode(false);

                entity.Property(e => e.TelefoneUsuario).IsUnicode(false);

                entity.HasOne(d => d.FkIdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.FkIdTipoUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__FK_idTi__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
